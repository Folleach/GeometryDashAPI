using System;
using System.Net;
using System.Text.RegularExpressions;

namespace GeometryDashAPI.Server
{
    public class ServerResponse<T>
    {
        private static readonly Regex StatusCodeMatcher = new Regex(@"^(-|)\d+$");
        
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
            var match = StatusCodeMatcher.Match(body);
            if (match.Success)
                GeometryDashStatusCode = int.Parse(match.Value);
            else
                value = (T)GeometryDashApi.GetStringParser(typeof(T))(body);
        }
        
        public T GetResultOrDefault()
        {
            return value;
        }

        [Obsolete]
        public string GetRawOrDefault()
        {
            return raw;
        }
    }
}