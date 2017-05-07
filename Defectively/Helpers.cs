#pragma warning disable 1591

using System;
using System.Linq;

namespace Defectively
{
    public static class Helpers
    {
        public static string GenerateRandomId(int length) {
            const string Characters = "abcdefghijklmnopqrstuvwxyz0123456789";
            var Random = new Random();
            return new string(Enumerable.Repeat(Characters, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
