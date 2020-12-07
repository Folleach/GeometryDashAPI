using System.Collections.Generic;

namespace GeometryDashAPI.Server.Models
{
    public class LevelCommentArray : List<LevelComment>
    {
        public Pagination Page { get; set; }

        public LevelCommentArray(string data)
        {
            var splitData = data.Split('#');
            Page = new Pagination(splitData[1]);
            var arr = splitData[0].Split('|');
            foreach (var item in arr)
                Add(new LevelComment(item));
        }
    }
}
