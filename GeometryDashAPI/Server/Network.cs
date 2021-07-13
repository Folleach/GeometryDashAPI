using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GeometryDashAPI.Server
{
    internal class Network
    {
        public Encoding DataEncoding { get; }
        public int Timeout { get; set; } = 30_000;

        public Network() : this(Encoding.ASCII)
        {
        }

        public Network(Encoding encoding)
        {
            DataEncoding = encoding;
        }
        
        public async Task<(HttpStatusCode statusCode, string body)> GetAsync(string path, IQuery query)
        {
            return await GetUseWebClient(path, query.BuildQuery());
        }

        private async Task<(HttpStatusCode statusCode, string body)> GetUseWebClient(string path, Parameters properties)
        {
            var client = WebRequest.Create($"http://boomlings.com{path}");
            client.ContentType = "application/x-www-form-urlencoded";
            client.Headers.Add("20", "*/*");
            client.Method = "POST";
            client.Timeout = Timeout;
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
