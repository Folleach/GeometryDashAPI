using System.Collections.Generic;

namespace GeometryDashAPI.Server.Models
{
    public class AccountCommentArray : List<AccountComment>
    {
        public Pagination Page { get; set; }

        public AccountCommentArray(string data)
        {
            var splitData = data.Split('#');
            Page = new Pagination(splitData[1]);
            var arr = splitData[0].Split('|');
            foreach (var item in arr)
                Add(new AccountComment(item));
        }
    }
}
