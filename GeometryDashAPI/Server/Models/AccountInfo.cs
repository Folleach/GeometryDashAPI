using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server.Models
{
    public class AccountInfo
    {
        public string Name { get; set; }
        public int UserID { get; set; }
        public int CubeID { get; set; }
        public int ShipID { get; set; }
        public int Color1 { get; set; }
        public int Color2 { get; set; }

        public AccountInfo(string data)
        {
            string[] arr = data.Split(':');
            for (int i = 0; i < arr.Length; i += 2)
            {
                switch (arr[i])
                {
                    case "1":
                        Name = arr[i + 1];
                        continue;
                    case "2":
                        UserID = int.Parse(arr[i + 1]);
                        continue;
                    case "10":
                        Color1 = int.Parse(arr[i + 1]);
                        continue;
                    case "11":
                        Color2 = int.Parse(arr[i + 1]);
                        continue;
                    case "21":
                        CubeID = int.Parse(arr[i + 1]);
                        continue;
                    case "22":
                        ShipID = int.Parse(arr[i + 1]);
                        continue;
                }
            }
        }
    }
}
