#pragma warning disable 1591

using System.Collections.Generic;

namespace Defectively.Command
{
    public class JsonCommand
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Definer { get; set; }
        public string Description { get; set; }
        public bool IgnoreCase { get; set; }
        public List<JsonParameter> Parameters { get; set; } = new List<JsonParameter>();
    }

    public class JsonParameter
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public ParameterType ParameterType { get; set; }
        public string ValueType { get; set; }
    }
}
