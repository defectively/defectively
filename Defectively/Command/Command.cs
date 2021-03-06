﻿#pragma warning disable 1591

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Defectively.Extension;

namespace Defectively.Command
{
    public class Command
    {
        public string Definer { get; set; }
        public string Description { get; set; }
        public string Namespace { get; set; }
        public bool IgnoreCase { get; set; }

        private string _Name;
        public string Name {
            get { return _Name; }
            set {
                if (!Regex.IsMatch(value, "[a-zA-Z0-9\\-]+")) {
                    throw new FormatException("Command names must be at least 1 char long and can only contain letters, numbers and -.");
                }
                _Name = value;
            }
        }

        public Command() { }

        public Command(string name, string @namespace, string definer = "\\/") {
            Definer = definer;
            if (string.IsNullOrEmpty(Definer)) {
                Definer = "\\/";
            }
            Name = name;
            Namespace = @namespace;
        }

        public Command(string name, IExtension extension, string definer = "\\/") {
            Definer = definer;
            if (string.IsNullOrEmpty(Definer)) {
                Definer = "\\/";
            }
            Name = name;
            Namespace = extension.Namespace;
        }

        private string Pattern {
            get {
                var Builder = new StringBuilder(Definer);
                var NamespaceParts = Namespace.Split('.').ToList();
                NamespaceParts.ForEach(n => Builder.Append("("));
                Builder.Append(NamespaceParts.Aggregate("", (total, current) => total + $"{current}\\.)?"));
                Builder.Remove(Builder.Length - 4, 4);
                Builder.Append(":)?");
                Builder.Append(Name);
                foreach (var Param in Parameters.OrderBy(pair => pair.Value.Index)) {
                    if (Param.Value.Type == ParameterType.Required) {
                        Builder.Append($" (?<{Param.Key}>[^ \\n\"]+|\"[^\\n\"]+\")"); // $" (?<{Param.Key}>[^ \\n]+)"
                    }
                    if (Param.Value.Type == ParameterType.Optional) {
                        Builder.Append($"( (?<{Param.Key}>.+))?"); // $"( (?<{Param.Key}>.+))?"
                    }
                }
                return Builder.ToString();
            }
        }

        public Dictionary<string, IParameter> Parameters { get; } = new Dictionary<string, IParameter>();

        public void AddParameter<T>(string name, int index, ParameterType parameterType) where T : IConvertible {
            if (!Regex.IsMatch(name, "[a-zA-Z0-9\\-]+")) {
                throw new FormatException("Parameter names must be at least 1 char long and can only contain letters, numbers and -.");
            }
            var Parameter = new Parameter<T> {
                Name = name,
                Index = index,
                Type = parameterType
            };
            Parameters.Add(name, Parameter);
        }

        public T Parse<T>(string content, string parameter) where T : IConvertible {
            if (!Regex.IsMatch(parameter, "[a-zA-Z0-9\\-]+")) {
                throw new FormatException("Parameter names must be at least 1 char long and can only contain letters, numbers and -.");
            }
            if (Validate(content)) {
                var Match = IgnoreCase ? Regex.Match(content, Pattern, RegexOptions.IgnoreCase) : Regex.Match(content, Pattern);
                try {
                    T Out;
                    if ((Parameters[parameter] as Parameter<T>).TryParse(Match.Groups[parameter].Value.Trim().Trim('"'), out Out)) {
                        return Out;
                    }
                    throw new FormatException($"The parameter {parameter} requires the type {Parameters[parameter].GetDefault().GetType()}.");
                } catch (NullReferenceException) {
                    throw new InvalidCastException($"The parameter {parameter} is not declared as {typeof(T)}");
                }
            }
            return default(T);
        }

        public bool Validate(string content) {
            if (string.IsNullOrEmpty(Definer)) {
                Definer = "\\/";
            }
            return IgnoreCase ? Regex.IsMatch(content, Pattern, RegexOptions.IgnoreCase) : Regex.IsMatch(content, Pattern);
        }

        public override string ToString() {
            var Builder = new StringBuilder();

            Builder.Append($"/<{Namespace}:>{Name}");
            foreach (var Pair in Parameters) {
                if (Pair.Value.Type == ParameterType.Required) {
                    Builder.Append($" ({Pair.Key})");
                } else {
                    Builder.Append($" [{Pair.Key}]");
                }
            }

            return Builder.ToString();
        }
    }
}
