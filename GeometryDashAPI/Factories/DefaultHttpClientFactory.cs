using System.Net.Http;

namespace GeometryDashAPI.Factories
{
    public class DefaultHttpClientFactory : IFactory<HttpClient>
    {
        public HttpClient Create()
        {
            return new HttpClient();
        }
    }
}
