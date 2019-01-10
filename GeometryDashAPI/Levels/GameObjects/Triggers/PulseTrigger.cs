using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;
using System.Text;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    public class PulseTrigger : Trigger
    {
        private bool _mainOnly;
        private bool _detailOnly;

        public int TargetID { get; set; }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public PulseModeType PulseMode { get; set; }
        public TargetType TargetType { get; set; }
        public bool Exclusive { get; set; }
        public float FadeIn { get; set; }
        public float Hold { get; set; }
        public float FadeOut { get; set; }
        public bool MainOnly
        {
            get => _mainOnly;
            set
            {
                _detailOnly = false;
                _mainOnly = true;
            }
        }
        public bool DetailOnly
        {
            get => _detailOnly;
            set
            {
                _mainOnly = false;
                _detailOnly = true;
            }
        }
        public HSV ValueHSV { get; set; }
        public short ColorID { get; set; }

        public PulseTrigger() : base(1006)
        {
            IsTrigger = true;
        }

        public PulseTrigger(string[] data)
        {
            for (int i = 0; i < data.Length; i += 2)
                LoadProperty(byte.Parse(data[i]), data[i + 1]);
        }

        public override void LoadProperty(byte key, string value)
        {
            switch (key)
            {
                case 51:
                    TargetID = int.Parse(value);
                    return;
                case 7:
                    Red = byte.Parse(value);
                    return;
                case 8:
                    Green = byte.Parse(value);
                    return;
                case 9:
                    Blue = byte.Parse(value);
                    return;
                case 48:
                    PulseMode = (PulseModeType)byte.Parse(value);
                    return;
                case 52:
                    TargetType = (TargetType)byte.Parse(value);
                    return;
                case 86:
                    Exclusive = GameConvert.StringToBool(value);
                    return;
                case 45:
                    FadeIn = GameConvert.StringToSingle(value);
                    return;
                case 46:
                    Hold = GameConvert.StringToSingle(value);
                    return;
                case 47:
                    FadeOut = GameConvert.StringToSingle(value);
                    return;
                case 65:
                    MainOnly = GameConvert.StringToBool(value);
                    return;
                case 66:
                    DetailOnly = GameConvert.StringToBool(value);
                    return;
                case 49:
                    ValueHSV = new HSV(value);
                    return;
                case 50:
                    ColorID = short.Parse(value);
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
            builder.Append($",51,{TargetID}");
            if (Red != 0)
                builder.Append($",7,{Red}");
            if (Green != 0)
                builder.Append($",8,{Green}");
            if (Blue != 0)
                builder.Append($",9,{Blue}");
            if (PulseMode != PulseModeType.Color)
                builder.Append($",48,{(byte)PulseMode}");
            if (TargetType != TargetType.Channel)
                builder.Append($",52,{(byte)TargetType}");
            if (Exclusive)
                builder.Append(",86,1");
            if (FadeIn != 0f)
                builder.Append($",45,{GameConvert.SingleToString(FadeIn)}");
            if (Hold != 0f)
                builder.Append($",46,{GameConvert.SingleToString(Hold)}");
            if (FadeOut != 0f)
                builder.Append($",47,{GameConvert.SingleToString(FadeOut)}");
            if (_mainOnly)
                builder.Append($",65,1");
            if (_detailOnly)
                builder.Append($",66,1");
            if (PulseMode == PulseModeType.HSV && !ValueHSV.IsDefault)
                builder.Append($",49,{ValueHSV.ToString()}");
            if (ColorID != 0)
                builder.Append($",50,{ColorID}");

            return builder.ToString();
        }
    }
}
