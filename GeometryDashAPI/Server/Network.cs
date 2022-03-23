using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GeometryDashAPI.Server
{
    public class Network
    {
        private readonly Func<HttpClientHandler> configurator;
        private readonly Func<string, bool> responseFilter;
        public Encoding DataEncoding { get; }
        public int Timeout { get; set; } = 30_000;

        public Network() : this(Encoding.ASCII)
        {
        }

        public Network(Encoding encoding, Func<HttpClientHandler> configurator = null, Func<string, bool> responseFilter = null)
        {
            DataEncoding = encoding;
            this.configurator = configurator;
            this.responseFilter = responseFilter;
        }
        
        public async Task<(HttpStatusCode statusCode, string body)> GetAsync(string path, IQuery query)
        {
            return await GetUseHttpClient(path, query.BuildQuery());
        }

        private async Task<(HttpStatusCode statusCode, string body)> GetUseHttpClient(string path, Parameters properties)
        {
            using var httpClient = new HttpClient(configurator == null ? new HttpClientHandler() : configurator());
            httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout);

            using var response = await httpClient.PostAsync($"http://www.boomlings.com{path}",
                new StringContent(properties.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded"));

            var result = await response.Content.ReadAsStringAsync();
            if (responseFilter != null && !responseFilter(result))
                throw new Exception("Invalid data by response filter");
            var statusCode = response.StatusCode;
            return (statusCode, result);
        }
    }
}
