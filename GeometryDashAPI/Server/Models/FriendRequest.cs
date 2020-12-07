using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server.Models
{
    public class FriendRequest
    {
        #region fields
        public int ID { get; private set; }
        public int AccountID { get; private set; }
        public string UserName { get; private set; }
        public string Reason { get; private set; }
        public string Timestamp { get; private set; }
        public bool Outcoming { get; private set; } = false;
        #endregion

        public FriendRequest(string data)
        {
            string[] arr = data.Split('#')[0].Split(':');
            for (int i = 0; i < arr.Length; i += 2)
            {
                switch (arr[i])
                {
                    case "1":
                        UserName = arr[i + 1];
                        break;
                    case "14":
                        Outcoming = arr[i + 1] == "1";
                        break;
                    case "16":
                        AccountID = int.Parse(arr[i + 1]);
                        break;
                    case "32":
                        ID = int.Parse(arr[i + 1]);
                        break;
                    case "35":
                        Reason = Encoding.ASCII.GetString(GameConvert.FromBase64(arr[i + 1]));
                        break;
                    case "37":
                        Timestamp = arr[i + 1];
                        break;
                }
            }
        }
    }
}
