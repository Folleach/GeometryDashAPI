using GeometryDashAPI.Levels.GameObjects.Default;
using System.Text;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    public class CountTrigger : Trigger
    {
        public int TargetGroupID { get; set; }
        public bool ActivateGroup { get; set; }
        public int TargetCount { get; set; }
        public int ItemID { get; set; }
        public bool MultiActivate { get; set; }

        public CountTrigger() : base(1611)
        {
        }

        public CountTrigger(string[] data)
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
                    MultiActivate = GameConvert.StringToBool(value);
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
            if (MultiActivate)
                builder.Append(",104,1");

            return builder.ToString();
        }
    }
}
