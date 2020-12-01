using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server.Models
{
    public class MessageArray : List<Message>
    {
        public MessageArray(string data, GameServer server, int accountID)
        {
            foreach (string msgData in data.Split('|')) {
                int id = 0;
                bool outcomming = false;

                string[] splitted = msgData.Split(':');
                for (int i = 0; i < splitted.Length; i += 2)
                {
                    if (splitted[i] == "1")
                    {
                        id = int.Parse(splitted[i + 1]);
                    } else if (splitted[i] == "9"){
                        outcomming = splitted[i + 1] == "1";
                    }
                }

                Add(server.GetMessage(id, accountID, outcomming));
            }
        }
    }
}
