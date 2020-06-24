using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GeometryDashAPI.Server
{
    internal class Network
    {
        public Encoding DataEncoding;

        public Network() : this(Encoding.ASCII)
        {
        }

        public Network(Encoding encoding)
        {
            DataEncoding = encoding;
        }

        public string Get(string path, IQuery query)
        {
            return GetUseWebClient(path, query.BuildQuery());
        }

        public async Task<string> GetAsync(string path, IQuery query)
        {
            string result = null;
            await Task.Run(() =>
            {
                result = Get(path, query);
            });
            return result;
        }

        private string GetUseWebClient(string path, Parameters properties)
        {
            WebRequest client = WebRequest.Create($"http://boomlings.com{path}");
            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            client.Headers[HttpRequestHeader.Accept] = "*/*";
            client.Method = "POST";
            byte[] data = DataEncoding.GetBytes(properties.ToString());
            client.GetRequestStream().Write(data, 0, data.Length);
            return new StreamReader(client.GetResponse().GetResponseStream()).ReadToEnd();
        }
    }
}
