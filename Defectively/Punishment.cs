#pragma warning disable 1591

using System;

namespace Defectively
{
    public class Punishment
    {
        public string AccountId { get; set; }
        public Enumerations.PunishmentType Type { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string CreatorId { get; set; }
        public string Id { get; set; }

        public Punishment() {
            Id = Helpers.GenerateRandomId(6);
        }

        public bool IsExceeded => EndDate.CompareTo(DateTime.Now) <= 0 && Type != Enumerations.PunishmentType.PermanentBann;

        
    }
}
