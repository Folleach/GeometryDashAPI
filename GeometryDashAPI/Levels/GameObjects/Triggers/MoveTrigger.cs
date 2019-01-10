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

        public MoveTrigger(string[] data)
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
                case 28:
                    MoveX = int.Parse(value);
                    return;
                case 29:
                    MoveY = int.Parse(value);
                    return;
                case 10:
                    Time = GameConvert.StringToSingle(value);
                    return;
                case 30:
                    EasingType = (Easing)byte.Parse(value);
                    return;
                case 85:
                    EasingTime = GameConvert.StringToSingle(value);
                    return;
                default:
                    base.LoadProperty(key, value);
                    return;
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()},51,{TargetGroupID},28,{MoveX},29,{MoveY},10,{GameConvert.SingleToString(Time)},30,{(byte)EasingType},85,{GameConvert.SingleToString(EasingTime)}";
        }
    }
}
