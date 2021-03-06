﻿#pragma warning disable 1591

using System;

namespace Defectively.Compatibility
{
    [AttributeUsage(AttributeTargets.Assembly)]
    // ReSharper disable once InconsistentNaming
    public sealed class LSClientVersionAttribute : Attribute, ILSVersionAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LSClientVersionAttribute" /> class.
        /// </summary>
        /// <param name="versionString">The version string to use.</param>
        public LSClientVersionAttribute(string versionString) {
            VersionString = versionString;
        }

        public string VersionString { get; }
    }
}
