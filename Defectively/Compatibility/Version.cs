﻿#pragma warning disable 1591

namespace Defectively.Compatibility
{
    public class Version
    {
        public virtual int Major { get; set; } = 2;
        public virtual int Minor { get; set; } = 2;
        public virtual int Patch { get; set; } = 26;
        public virtual VersioningProfiler.Suffixes Suffix { get; set; } = VersioningProfiler.Suffixes.none;
        public virtual string ReleaseDate { get; set; } = "17w05";
        public virtual string Commit { get; set; } = "b5b2d9d"; // #festival-version-control new
        public virtual string SupportedVersion { get; set; }

        public string ToShortString() {
            return $"{Major}.{Minor}.{Patch}";
        }

        public string ToMediumString() {
            return $"{Major}.{Minor}.{Patch}{(Suffix == VersioningProfiler.Suffixes.none ? "" : $"-{Suffix}")} [{Commit}]";
        }

        public string ToLongString() {
            return $"{ToMediumString()} @{ReleaseDate}";
        }
    }
}