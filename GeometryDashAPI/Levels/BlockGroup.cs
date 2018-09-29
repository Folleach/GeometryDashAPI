using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Levels
{
    public class BlockGroup : List<int>
    {
        public BlockGroup() : base()
        {

        }

        public BlockGroup(string data) : base()
        {
            foreach (string id in data.Split('.'))
                Add(int.Parse(id));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            bool first = true;
            foreach (int id in base.ToArray())
            {
                if (first)
                {
                    builder.Append(id);
                    first = false;
                }
                else
                    builder.Append($".{id}");
            }
            return builder.ToString();
        }
    }
}
