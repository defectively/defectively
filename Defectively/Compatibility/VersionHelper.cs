#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Defectively.Compatibility
{
    public static class VersionHelper
    {
        public static Dictionary<DevelopmentStage, string> StageCharacters = new Dictionary<DevelopmentStage, string> {
            {DevelopmentStage.Alpha, "A"},
            {DevelopmentStage.Beta, "B"},
            {DevelopmentStage.ReleaseCandidate, "RC"},
            {DevelopmentStage.Release, "R"}
        };

        /// <summary>
        ///     Initializes a new instance of the <see cref="Version" /> class.
        /// </summary>
        /// <param name="versionString">The version string to use.</param>
        /// <returns></returns>
        public static Version GetVersionFromString(string versionString) {
            var Parts = versionString.Split('.');

            if (!StageCharacters.ContainsValue(Parts[3]))
                throw new ArgumentException($"\"{versionString}\" does not represent a valid version.");

            foreach (var Pair in StageCharacters) {
                if (Pair.Value == Parts[3].ToUpper()) {
                    Parts[3] = ((int) Pair.Key).ToString();
                }
            }

            var VersionString = Parts.Aggregate("", (current, next) => $"{current}.{next}").Remove(0, 1);

            return new Version(VersionString);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Version" /> class.
        /// </summary>
        /// <param name="assembly">The assembly to load the version from.</param>
        /// <returns></returns>
        public static Version GetVersionFromAssembly(Assembly assembly) {
            var VersionAttribute = assembly.GetCustomAttributes(false).OfType<VersionAttribute>().SingleOrDefault();

            if (VersionAttribute == null)
                throw new ArgumentException("The VersionAttribute isn't defined in the assembly.");

            return GetVersionFromString(VersionAttribute.VersionString);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Version" /> class.
        /// </summary>
        /// <typeparam name="T">The type of LeastSupportedVersion to load.</typeparam>
        /// <param name="assembly">The assembly to load the version from.</param>
        /// <returns></returns>
        // ReSharper disable InconsistentNaming once
        public static Version GetLSVersionFromAssembly<T>(Assembly assembly) where T : ILSVersionAttribute {
            var VersionAttribute = assembly.GetCustomAttributes(false).OfType<T>().SingleOrDefault();

            if (VersionAttribute == null)
                throw new ArgumentException("The LSVersionAttribute isn't defined in the assembly.");

            return GetVersionFromString(VersionAttribute.VersionString);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Version" /> class with the Core version.
        /// </summary>
        public static Version GetVersionFromCore => GetVersionFromAssembly(Assembly.GetAssembly(typeof(VersionHelper)));
        
        public static string GetFullStringFromVersion(Version version) {
            var Parts = version.ToString(4).Split('.');

            foreach (var Pair in StageCharacters) {
                if ((int) Pair.Key == int.Parse(Parts[3])) {
                    Parts[3] = Pair.Key.ToString();
                    break;
                }
            }

            // Travis-CI Compatibility Fix
            int _;
            if (int.TryParse(Parts[3], out _)) {
                throw new ArgumentException($"\"{version}\" does not represent a valid version.");
            }

            return $"{Parts[0]}.{Parts[1]}.{Parts[2]} {Parts[3]}";
        }

        public static string GetShortStringFromVersion(Version version) {
            var Parts = version.ToString(4).Split('.');

            foreach (var Pair in StageCharacters) {
                if ((int) Pair.Key == int.Parse(Parts[3])) {
                    Parts[3] = Pair.Value;
                    break;
                }
            }

            if (int.TryParse(Parts[3], out int _)) {
                throw new ArgumentException($"\"{version}\" does not represent a valid version.");
            }

            return Parts.Aggregate("", (current, next) => $"{current}.{next}").Remove(0, 1);
        }

        public static string GetFullStringFromAssembly(Assembly assembly) {
            return GetFullStringFromVersion(GetVersionFromAssembly(assembly));
        }

        public static string GetFullStringFromCore() {
            return GetFullStringFromVersion(GetVersionFromCore);
        }

        /// <summary>
        ///     Returns whether the <see cref="Version"/> is supported by an other <see cref="Version"/>.
        /// </summary>
        /// <param name="version">The version that should be supported by the second.</param>
        /// <param name="otherVersion">The version that should support the first.</param>
        /// <returns></returns>
        public static bool IsSupportedBy(this Version version, Version otherVersion) {
            return version.CompareTo(otherVersion) >= 0;
        }
    }
}