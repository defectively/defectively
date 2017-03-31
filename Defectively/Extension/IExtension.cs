﻿#pragma warning disable 1591

using System.Collections.Generic;
using Defectively.Compatibility;

namespace Defectively.Extension
{
    public interface IExtension
    {
        string Author { get; }
        bool ClientInstance { get; }
        Dictionary<string, Command.Command> Commands { get; set; }
        string Description { get; }
        bool Disabled { get; set; }
        IEnumerable<Listener> ServerListeners { get; }
        IEnumerable<Listener> ClientListeners { get; }
        string Name { get; }
        string Namespace { get; }
        bool StorageNeeded { get; }
        string StoragePath { get; set; }
        string Path { get; set; }
        Version Version { get; }

        void OnEnable();
        void OnRun();
        void OnDisable();
    }
}
