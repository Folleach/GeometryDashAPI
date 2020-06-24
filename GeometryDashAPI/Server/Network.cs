using System.Collections.Generic;
using System.Net.Sockets;
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
            return GetUseRawMethod(path, query.BuildQuery());
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

        private string GetUseRawMethod(string path, Parameters parameters)
        {
            string content = parameters.ToString();
            // Head
            StringBuilder request = new StringBuilder();
            request.Append($"POST {path} HTTP/1.1\r\n");
            request.Append("Host: www.boomlings.com\r\n");
            request.Append("Accept: */*\r\n");
            request.Append($"Content-Length: {content.Length}\r\n");
            request.Append($"Content-Type: application/x-www-form-urlencoded\r\n\r\n");
            request.Append(content);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("www.boomlings.com", 80);
            socket.Send(DataEncoding.GetBytes(request.ToString()));

            List<byte> buffer = new List<byte>();
            byte[] buf;
            int byteRead = 0;
            do
            {
                buf = new byte[socket.Available];
                byteRead = socket.Receive(buf);
                buffer.AddRange(buf);
            }
            while (byteRead != 0 || socket.Available != 0);

            string response = DataEncoding.GetString(buffer.ToArray());
            int endHeadIndex = response.IndexOf("\r\n\r\n");
            return response.Remove(0, endHeadIndex + 4);
        }
    }
}
