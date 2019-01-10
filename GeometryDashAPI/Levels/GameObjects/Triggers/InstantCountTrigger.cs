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

        public InstantCountTrigger(string[] data)
        {
            for (int i = 0; i < data.Length; i += 2)
                LoadProperty(byte.Parse(data[i]), data[i + 1]);
        }

        public override void LoadProperty(byte key, string value)
        {
            switch (key)
            {
                case 51:
                    TargetGroupID = int.Parse(value);
                    return;
                case 56:
                    ActivateGroup = GameConvert.StringToBool(value);
                    return;
                case 77:
                    TargetCount = int.Parse(value);
                    return;
                case 80:
                    ItemID = int.Parse(value);
                    return;
                case 104:
                    Condition = (ConditionType)byte.Parse(value);
                    return;
                default:
                    base.LoadProperty(key, value);
                    return;
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
