using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    public class RotateTrigger : Trigger
    {
        public int TargetGroupID { get; set; }
        public int CenterGroupID { get; set; }
        public bool LockObjectRotation { get; set; }
        public int Degrees { get; set; } = 0;
        public int Times360 { get; set; } = 0;
        public float Time { get; set; } = 0.5f;
        public Easing EasingType { get; set; } = Easing.None;
        public float EasingTime { get; set; } = 2f;

        public RotateTrigger() : base(1346)
        {
        }

        public RotateTrigger(string[] data)
        {
            for (int i = 0; i < data.Length; i += 2)
                LoadProperty(byte.Parse(data[i]), data[i + 1]);
        }

        public void LoadProperty(byte key, string value)
        {
            switch (key)
            {
                case 51:
                    TargetGroupID = int.Parse(value);
                    return;
                case 71:
                    CenterGroupID = int.Parse(value);
                    return;
                case 70:
                    LockObjectRotation = GameConvert.StringToBool(value);
                    return;
                case 68:
                    Degrees = int.Parse(value);
                    return;
                case 69:
                    Times360 = int.Parse(value);
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
                    //base.LoadProperty(key, value);
                    return;
            }
        }
        
        public override string ToString()
        {
            return $"{base.ToString()}" +
                $",51,{TargetGroupID}" +
                $",71,{CenterGroupID}" +
                $",70,{(LockObjectRotation == true? "1": "0")}" +
                $",68,{Degrees}" +
                $",69,{Times360}" +
                $",10,{GameConvert.SingleToString(Time)}" +
                $",30,{(byte)EasingType}" +
                $",85,{GameConvert.SingleToString(EasingTime)}";
        }
    }
}
