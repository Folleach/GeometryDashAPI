using System;
using System.Net;

namespace GeometryDashAPI.Server
{
    public class GameClient : WebClient
    {
        private readonly int timeoutMs;

        public GameClient(int timeoutMs)
        {
            this.timeoutMs = timeoutMs;
        }
        
        protected override WebRequest GetWebRequest(Uri uri)
        {
            var request = base.GetWebRequest(uri);
            if (request == null)
                return null;
            request.Timeout = timeoutMs;
            return request;

        }
    }
}
