using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server.Models
{
    public class Message
    {
        #region fields
        public int ID { get; private set; }
        public int AccountID { get; private set; }
        public string UserName { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
        public string Timestamp { get; private set; }
        public bool Outcoming { get; private set; } = false;
        #endregion

        public Message(string data)
        {
            string[] arr = data.Split(':');
            for (int i = 0; i < arr.Length; i += 2)
            {
                switch (arr[i])
                {
                    case "1":
                        ID = int.Parse(arr[i + 1]);
                        break;
                    case "2":
                        AccountID = int.Parse(arr[i + 1]);
                        break;
                    case "4":
                        Subject = Encoding.ASCII.GetString(GameConvert.FromBase64(arr[i + 1]));
                        break;
                    case "5":
                        Body = Crypt.XOR(Encoding.ASCII.GetString(GameConvert.FromBase64(arr[i + 1])), "14251");
                        break;
                    case "6":
                        UserName = arr[i + 1];
                        break;
                    case "7":
                        Timestamp = arr[i + 1];
                        break;
                    case "9":
                        Outcoming = arr[i + 1] == "1";
                        break;
                }
            }
        }
    }
}
