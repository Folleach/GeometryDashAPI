using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    public class MoveTrigger : Trigger
    {
        public int TargetGroupID { get; set; }
        public int MoveX { get; set; }
        public int MoveY { get; set; }
        public float Time { get; set; } = 0.5f;
        public Easing EasingType { get; set; } = Easing.None;
        public float EasingTime { get; set; } = 2f;

        public MoveTrigger() : base(901)
        {
        }

        public MoveTrigger(string[] data) : base(data)
        {
            for (int i = 0; i < data.Length; i += 2)
            {
                switch (data[i])
                {
                    case "51": TargetGroupID = int.Parse(data[i + 1]);
                        break;
                    case "28": MoveX = int.Parse(data[i + 1]);
                        break;
                    case "29": MoveY = int.Parse(data[i + 1]);
                        break;
                    case "10": GameConvert.StringToSingle(data[i + 1]);
                        break;
                    case "30": EasingType = (Easing)byte.Parse(data[i + 1]);
                        break;
                    case "85": GameConvert.StringToSingle(data[i + 1]);
                        break;
                    default:
                        break;
                }
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()},51,{TargetGroupID},28,{MoveX},29,{MoveY},10,{GameConvert.SingleToString(Time)},30,{(byte)EasingType},85,{GameConvert.SingleToString(EasingTime)}";
        }
    }
}
