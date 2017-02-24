#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Defectively.Command
{
    public static class CommandFactory
    {
        public static Command CreateCommandFromJson(JsonCommand structure) {
            var Command = new Command();

            Command.Name = structure.Name;
            Command.Namespace = structure.Namespace;
            Command.Definer = structure.Definer;
            Command.Description = structure.Description;
            Command.IgnoreCase = structure.IgnoreCase;

            foreach (var Parameter in structure.Parameters) {
                if (Parameter.ValueType == "string") {
                    Command.AddParameter<string>(Parameter.Name, Parameter.Index, Parameter.ParameterType);
                } else if (Parameter.ValueType == "bool") {
                    Command.AddParameter<bool>(Parameter.Name, Parameter.Index, Parameter.ParameterType);
                } else if (Parameter.ValueType == "double") {
                    Command.AddParameter<double>(Parameter.Name, Parameter.Index, Parameter.ParameterType);
                } else if (Parameter.ValueType == "int") {
                    Command.AddParameter<int>(Parameter.Name, Parameter.Index, Parameter.ParameterType);
                }
            }

            return Command;
        }
    }
}
