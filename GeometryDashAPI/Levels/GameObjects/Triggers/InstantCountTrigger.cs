using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;
using System.Text;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    public class InstantCountTrigger : Trigger
    {
        public int TargetGroupID { get; set; }
        public bool ActivateGroup { get; set; }
        public int TargetCount { get; set; }
        public int ItemID { get; set; }
        public ConditionType Condition { get; set; }

        public InstantCountTrigger() : base(1811)
        {
        }

        public InstantCountTrigger(string[] data) : base(data)
        {
            for (int i = 0; i < data.Length; i += 2)
            {
                switch (data[i])
                {
                    case "51": TargetGroupID = int.Parse(data[i + 1]);
                        continue;
                    case "56": ActivateGroup = GameConvert.StringToBool(data[i + 1]);
                        continue;
                    case "77": TargetCount = int.Parse(data[i + 1]);
                        continue;
                    case "80": ItemID = int.Parse(data[i + 1]);
                        continue;
                    case "104": Condition = (ConditionType)byte.Parse(data[i + 1]);
                        continue;
                    default:
                        continue;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(base.ToString());
            builder.Append($",51,{TargetGroupID}");
            builder.Append($",80,{ItemID}");
            if (ActivateGroup)
                builder.Append(",56,1");
            if (TargetCount != 0)
                builder.Append($",77,{TargetCount}");
            if (Condition != ConditionType.Equals)
                builder.Append($",88,{(int)Condition}");

            return builder.ToString();
        }
    }
}
