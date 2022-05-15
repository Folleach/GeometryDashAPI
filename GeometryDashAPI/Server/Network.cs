using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GeometryDashAPI.Factories;

namespace GeometryDashAPI.Server
{
    public class Network
    {
        private readonly IFactory<HttpClient> httpClientFactory;
        private readonly Func<string, bool> responseFilter;
        public Encoding DataEncoding { get; }
        public int Timeout { get; set; } = 30_000;

        public Network() : this(Encoding.ASCII)
        {
        }

        public Network(Encoding encoding, IFactory<HttpClient> httpClientFactory = null, Func<string, bool> responseFilter = null)
        {
            DataEncoding = encoding;
            this.httpClientFactory = httpClientFactory ?? new DefaultHttpClientFactory();
            this.responseFilter = responseFilter;
        }
        
        public async Task<(HttpStatusCode statusCode, string body)> GetAsync(string path, IQuery query)
        {
            return await GetUseHttpClient(path, query.BuildQuery());
        }

        private async Task<(HttpStatusCode statusCode, string body)> GetUseHttpClient(string path, Parameters properties)
        {
            using var httpClient = httpClientFactory.Create();
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
