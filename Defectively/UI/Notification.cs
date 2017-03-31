#pragma warning disable 1591

using System;

namespace Defectively.UI
{
    public class Notification
    {
        private delegate void VoidDelegate();
        
        public string Title { get; set; } = "Defectively";
        public string Content { get; set; }
        public int Timeout { get; set; } = 2000;
        public Delegate CallbackDelegate { get; set; }

        public Notification() { }

        public Notification(string title, string content, int timeout = 2000) {
            Title = title;
            Content = content;
            Timeout = timeout;
        }
        
        public Notification(string title, string content, int timeout, Action callback) {
            Title = title;
            Content = content;
            Timeout = timeout;
            CallbackDelegate = new VoidDelegate(callback);
        }
    }
}
