using System.Collections.Generic;

namespace GeometryDashAPI.Server.Models
{
    public class AccountCommentArray : List<AccountComment>
    {
        public int OnlineCount { get; private set; }
        public int RangeIn { get; private set; }
        public int RangeOut { get; private set; }

        public AccountCommentArray(string data)
        {
            var splitData = data.Split('#');
            var arr = splitData[0].Split('|');
            ParseAdditionalInfo(splitData[1].Split(':'));
            foreach (var item in arr)
                Add(new AccountComment(item));
        }

        private void ParseAdditionalInfo(string[] data)
        {
            OnlineCount = int.Parse(data[0]);
            RangeIn = int.Parse(data[1]);
            RangeOut = int.Parse(data[2]);
        }
    }
}
