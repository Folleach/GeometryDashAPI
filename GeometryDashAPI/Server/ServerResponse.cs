using System;
using System.Net;

namespace GeometryDashAPI.Server
{
    public class ServerResponse<T> where T : IGameObject
    {
        public HttpStatusCode HttpStatusCode { get; }

        /// <summary>
        /// 0 by success
        /// </summary>
        public int GeometryDashStatusCode { get; }

        private readonly T value;
        private readonly string raw;

        public ServerResponse(HttpStatusCode statusCode, string body)
        {
            HttpStatusCode = statusCode;
            raw = body;
            if (HttpStatusCode != HttpStatusCode.OK && ServerResponseHelper.ErrorCodeRegex.Match(body).Success)
            {
                GeometryDashStatusCode = -1; // because "error code" looks like error from cloudflare (GeometryDash uses cloudflare as a proxy)
                return;
            }
            var match = ServerResponseHelper.StatusCodeRegex.Match(body);
            if (match.Success)
            {
                GeometryDashStatusCode = int.Parse(match.Value);
                return;
            }
            value = GeometryDashApi.Serializer.Decode<T>(body.AsSpan());
        }

        public T GetResultOrDefault()
        {
            return value;
        }

        public string GetRawOrDefault()
        {
            return raw;
        }
    }
}
