using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GeometryDashAPI.Server
{
    public class Network
    {
        private readonly Action<HttpWebRequest> configurator;
        public Encoding DataEncoding { get; }
        public int Timeout { get; set; } = 30_000;

        public Network() : this(Encoding.ASCII)
        {
        }

        public Network(Encoding encoding, Action<HttpWebRequest> configurator = null)
        {
            DataEncoding = encoding;
            this.configurator = configurator;
        }
        
        public async Task<(HttpStatusCode statusCode, string body)> GetAsync(string path, IQuery query)
        {
            return await GetUseWebClient(path, query.BuildQuery());
        }

        private async Task<(HttpStatusCode statusCode, string body)> GetUseWebClient(string path, Parameters properties)
        {
            var client = (HttpWebRequest)WebRequest.Create($"http://www.boomlings.com{path}");
            client.ContentType = "application/x-www-form-urlencoded";
            client.Method = "POST";
            client.Timeout = Timeout;
            client.Accept = "*/*";
            configurator?.Invoke(client);
            var data = DataEncoding.GetBytes(properties.ToString());
            var requestStream = await client.GetRequestStreamAsync();
            await requestStream.WriteAsync(data, 0, data.Length);
            var response = (HttpWebResponse)await client.GetResponseAsync();
            var responseStream = response.GetResponseStream();
            var result = responseStream == null ? null : await new StreamReader(responseStream).ReadToEndAsync();
            var statusCode = response.StatusCode;
            response.Close();
            return (statusCode, result);
        }
    }
}
