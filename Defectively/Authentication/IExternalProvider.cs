#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defectively.Authentication
{
    public interface IExternalProvider
    {
        string Guid { get; set; }
        string Name { get; set; }
        bool Authenticate(string[] data);
        bool Authorize(string[] data);
    }
}
