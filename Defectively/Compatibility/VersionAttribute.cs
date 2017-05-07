#pragma warning disable 1591

using System;

namespace Defectively.Compatibility
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class VersionAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VersionAttribute" /> class.
        /// </summary>
        /// <param name="versionString">The version string to use.</param>
        public VersionAttribute(string versionString) {
            VersionString = versionString;
        }

        public string VersionString { get; }
    }
}
