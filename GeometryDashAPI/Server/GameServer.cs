using GeometryDashAPI.Server.Enums;
using GeometryDashAPI.Server.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeometryDashAPI.Server
{
    public class GameServer
    {
        public Encoding DataEncoding { get; }

        public short GameVersion { get; set; } = 21;
        public short BinaryVersion { get; set; } = 35;
        public string Secret { get; set; } = "Wmfd2893gb7";
        public string UDID { get; set; } = "00000000-ffff-dddd-5555-123456789abc";
        public int UUID { get; set; } = 1;

        #region Constructors
        public GameServer() : this(Encoding.ASCII)
        {
        }
        public GameServer(Encoding encoding)
        {
            DataEncoding = encoding;
        }
        #endregion

        #region Http
        public string Get(string path, params Property[] properties)
        {
            //Content
            StringBuilder requestContent = new StringBuilder();
            bool cont = false;
            for (int i = 0; i < properties.Length; i++)
            {
                if (cont)
                    requestContent.Append('&');
                cont = true;
                requestContent.Append(properties[i].Key);
                requestContent.Append('=');
                requestContent.Append(properties[i].Value);
            }
            string content = requestContent.ToString();
            int contentLenght = content.Length;
            //Head
            StringBuilder request = new StringBuilder();
            request.Append($"POST {path} HTTP/1.1\r\n");
            request.Append("Host: www.boomlings.com\r\n");
            request.Append("Accept: */*\r\n");
            request.Append($"Content-Length: {contentLenght}\r\n");
            request.Append($"Content-Type: application/x-www-form-urlencoded\r\n\r\n");
            //End head
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

            //Remove header
            string response = DataEncoding.GetString(buffer.ToArray());
            int indexHead = response.IndexOf("\r\n\r\n");

            return response.Remove(0, indexHead + 4);
        }
        public async Task<string> GetAsync(string path, params Property[] properties)
        {
            string response = "";
            await Task.Run(() =>
            {
                response = Get(path, properties);
            });
            return response;
        }
        #endregion

        #region Game requests
        public PlayerInfoArray GetTop(TopType type, int count)
        {
            string response = Get("/database/getGJScores20.php",
                new Property("gameVersion", 21),
                new Property("binaryVersion", 35),
                new Property("gdw", 0),
                new Property("udid", UDID),
                new Property("uuid", UUID),
                new Property("type", type.GetAttribute<TopAttribute>().OriginalName),
                new Property("count", count),
                new Property("secret", "Wmfd2893gb7"));
            PlayerInfoArray players = new PlayerInfoArray();
            players.Load(response);
            return players;
        }

        public LevelInfoArray GetLevels(GetLevelsQuery getLevelsQuery)
        {
            List<Property> properties = new List<Property>(getLevelsQuery.BuildQuery());

            properties.Add(new Property("gameVersion", 21));
            properties.Add(new Property("binaryVersion", 35));
            properties.Add(new Property("gdw", 0));
            properties.Add(new Property("secret", "Wmfd2893gb7"));

            string response = Get("/database/getGJLevels21.php", properties.ToArray());
            //Console.WriteLine(response);

            LevelInfoArray levels = new LevelInfoArray();
            levels.Load(response);
            return levels;
        }

        #endregion
    }
}
