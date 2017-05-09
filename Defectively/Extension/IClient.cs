#pragma warning disable 1591

using System.Windows.Forms;
using Defectively.UI;

namespace Defectively.Extension
{
    public interface IClient
    {
        void DisplayForm(Form form);
        void SendPacketToServer(string packet);
        string Serialize(object content, bool indented);
        T Deserialize<T>(string content);
        void ShowNotification(Notification notification);
        void DisplayMessagePacket(MessagePacket packet);
    }
}
