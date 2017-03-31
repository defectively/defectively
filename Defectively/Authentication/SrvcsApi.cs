#pragma warning disable 1591

using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Defectively.Authentication
{
    public static class SrvcsApi
    {
        public static string Call(string action, string account, string data) {
            var Buffer = Encoding.ASCII.GetBytes($"action={action}&account={account}&data={data}");

            var Request = WebRequest.Create("https://srvcs.festival.ml/api/");
            Request.Method = "POST";
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.ContentLength = Buffer.Length;

            using (var Stream = Request.GetRequestStream()) {
                Stream.Write(Buffer, 0, Buffer.Length);
            }

            string Content;

            using (var Response = Request.GetResponse()) {
                using (var ResponseStream = Response.GetResponseStream()) {
                    using (var Reader = new StreamReader(ResponseStream)) {
                        Content = Reader.ReadToEnd();
                    }
                }
            }

            return Content;
        }

        public static async Task<string> CallAsync(string action, string account, string data) {
            var Buffer = Encoding.ASCII.GetBytes($"action={action}&account={account}&data={data}");

            var Request = WebRequest.Create("https://srvcs.festival.ml/api/");
            Request.Method = "POST";
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.ContentLength = Buffer.Length;

            using (var Stream = Request.GetRequestStream()) {
                Stream.Write(Buffer, 0, Buffer.Length);
            }

            string Content;

            var Response = await Request.GetResponseAsync();
            using (var ResponseStream = Response.GetResponseStream()) {
                using (var Reader = new StreamReader(ResponseStream)) {
                    Content = Reader.ReadToEnd();
                }
            }
            Response.Dispose();

            return Content;
        }
    }

    public class SrvcsApiResult
    {
        public string Result { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string RankId { get; set; }
    }
}
