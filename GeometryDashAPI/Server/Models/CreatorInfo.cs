using System;
using System.Collections.Generic;
using System.Text;
using GeometryDashAPI.Server.Enums;

namespace GeometryDashAPI.Server.Models
{
    public class CreatorInfo
    {
        #region fields
        public int UserID { get; private set; }
        public string UserName { get; private set; }
        public List<string> WithoutLoaded { get; private set; } = new List<string>();
        #endregion

        public CreatorInfo()
        {

        }

        public CreatorInfo(string data)
        {
            Load(data);
        }

        public void Load(string data)
        {
            string[] splitedData = data.Split(':');

            for (int i = 0; i < splitedData.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        UserID = int.Parse(splitedData[i]);
                        break;
                    case 1:
                        UserName = splitedData[i];
                        break;
                    default:
                        WithoutLoaded.Add(splitedData[i]);
                        break;
                }
            }
        }

        public override string ToString()
        {
            return $"{UserID} - {UserName}";
        }
    }
}
