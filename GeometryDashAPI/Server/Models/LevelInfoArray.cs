using System;
using System.Collections.Generic;
using System.Text;
using GeometryDashAPI.Server.Enums;

namespace GeometryDashAPI.Server.Models
{
    public class LevelInfoArray : List<LevelInfo>
    {
        string data;

        public void Load(string data)
        {
            string[] buffer = data.Split('#');
            List<string> splitData = new List<string>();

            foreach (string s in buffer)
            {
                splitData.AddRange(s.Split('|'));
            }
            for (int i = 0; i < splitData.Count - 1; i++)
            {
                Add(new LevelInfo(splitData[i]));
            }
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                result.Append(this[i].Data);
                result.Append("\n");
            }
            this.data = result.ToString();
        }
        public override string ToString()
        {
            return data;
        }
    }
}
