#pragma warning disable 1591

namespace Defectively
{
    public class ServerMetaData
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public string OwnerId { get; set; }
        public string OperatorWebsiteUrl { get; set; }
        public bool AcceptsGuests { get; set; }
        public bool AcceptsRegistration { get; set; }
        public bool RequiresInvitation { get; set; }
        public bool IsLockdown { get; set; }
        public string Version { get; set; }
        public string SVersion { get; set; }
        public string CVersion { get; set; }
    }
}
