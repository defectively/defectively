#pragma warning disable 1591

using System.Collections.Generic;

namespace Defectively
{
    public class Severity
    {
        public string Description { get; set; }
        public List<string> Values { get; set; } = new List<string>();
        public string Color { get; set; }
        public int Level { get; set; }
    }
}
