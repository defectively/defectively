#pragma warning disable 1591

using System;
using Defectively.Extension;

namespace Defectively
{
    public enum Event
    {
        /// <summary>
        /// Gets fired when the <see cref="IServer"/> starts.
        /// </summary>
        ServerStarted,

        /// <summary>
        /// Gets fired when the <see cref="IServer"/> receives a new <see cref="Connection"/>.
        /// </summary>
        ConnectionEstablished,

        /// <summary>
        /// Gets fired when a new <see cref="Account"/> is about to connect.
        /// </summary>
        ClientConnect,

        /// <summary>
        /// Gets fired when a new <see cref="Account"/> connected.
        /// </summary>
        ClientConnected,

        /// <summary>
        /// Gets fired when a new <see cref="Account"/> disconnected.
        /// </summary>
        ClientDisconnected,

        /// <summary>
        /// Gets fired when an <see cref="Account"/> send a message.
        /// </summary>
        ClientMessageReceived,

        /// <summary>
        /// Gets fired when the <see cref="IServer"/> registered a console input.
        /// </summary>
        ConsoleInputReceived,

        /// <summary>
        /// Gets fired when the <see cref="IServer"/> stopped.
        /// </summary>
        ServerStopped,

        PunishmentRecorded,
        PunishmentExceeded,
        ChannelCreated,
        ClientChannelChanged,
        ClientRankChanged,
        ChannelClosed,
        ClientBalanceChanged,

        /// <summary>
        /// Gets fired when external events are invoked.
        /// </summary>
        Dynamic
    }

    /// <summary>
    /// Contains pre-defined delegates for all <see cref="Event"/>s.
    /// </summary>
    public class EventDelegates
    {
        public delegate void OnServerStarted(string address);
        public delegate void OnServerStopped();
        public delegate void OnChannelCreated(string id);
        public delegate void OnChannelClosed(string id);
        public delegate void OnClientBalanceChanged(string id, long balance);
        public delegate void OnClientChannelChanged(string accountId, string channelId);
        public delegate void OnClientConnect(string address, string id, string parameters);
        public delegate void OnClientConnected(string id);
        public delegate void OnClientDisconnected(string id);
        public delegate void OnClientMessageReceived(string id, string content, string channelId);
        public delegate void OnClientRankChanged(string accountId, string rankId);
        public delegate void OnConnectionEstablished(string address, string id, string parameters, string password);
        public delegate void OnConsoleInputReceived(string input);
        public delegate void OnServerDynamicEvent(DynamicEvent e);
        public delegate void OnClientDynamicEvent(DynamicEvent e);
        public delegate void OnPunishmentExceeded(Punishment punishment);
        public delegate void OnPunishmentRecorded(Punishment punishment);
    }

    /// <summary>
    /// Contains information about a <see cref="Event.Dynamic"/> event.
    /// </summary>
    public class DynamicEvent
    {
        /// <summary>
        /// The internal identifier of the <see cref="Account"/> invoking this event.
        /// </summary>
        public string EndpointId { get; set; }

        /// <summary>
        /// The <see cref="IExtension.Namespace"/> of the <see cref="IExtension"/> invoking this event.
        /// </summary>
        public string Invoker { get; set; }

        /// <summary>
        /// The handler of this event.
        /// </summary>
        public string EventHandler { get; set; }

        /// <summary>
        /// The parameters of this event.
        /// </summary>
        public object[] Parameters { get; set; }

        /// <summary>
        /// Used to store answers from the listener.
        /// </summary>
        [Obsolete]
        public object CallbackReturn { get; set; }

        /// <summary>
        /// Empty constructor used for deserialization.
        /// </summary>
        public DynamicEvent() { }

        /// <summary>
        /// Creates a new <see cref="DynamicEvent"/>.
        /// </summary>
        /// <param name="handler">The handler of this event.</param>
        public DynamicEvent(string handler) {
            EventHandler = handler;
        }

        /// <summary>
        /// Creates a new <see cref="DynamicEvent"/>.
        /// </summary>
        /// <param name="handler">The handler of this event.</param>
        /// <param name="invoker">The invoker of this event.</param>
        public DynamicEvent(string handler, string invoker) {
            EventHandler = handler;
            Invoker = invoker;
        }

        /// <summary>
        /// Creates new <see cref="DynamicEvent"/>.
        /// </summary>
        /// <param name="handler">The handler of this event.</param>
        /// <param name="invoker">The invoker of this event.</param>
        /// <param name="params">The parameters of this event.</param>
        public DynamicEvent(string handler, string invoker, params object[] @params) {
            EventHandler = handler;
            Invoker = invoker;
            Parameters = @params;
        }


        /// <summary>
        /// Converts this event into a <see cref="string"/> eligable to be sent from an <see cref="IClient"/>.
        /// </summary>
        /// <returns></returns>
        public string ToServer() {
            return string.Join("|", Enumerations.Action.Extension, ExtensionPool.Client.Serialize(this, false));
        }

        /// <summary>
        /// Converts this event into a <see cref="string"/> eligable to be sent from an <see cref="IServer"/>.
        /// </summary>
        /// <returns></returns>
        public string ToClient() {
            return string.Join("|", Enumerations.Action.Extension, ExtensionPool.Server.Serialize(this, false));
        }
    }
}
