using GeometryDashAPI.Levels.GameObjects.Default;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    public class ColorTrigger : Trigger
    {
        [GameProperty("23", 0, true)]
        public short ColorID { get; set; }
        [GameProperty("7", 255, true)]
        public byte Red { get; set; } = 255;
        [GameProperty("8", 255, true)]
        public byte Green { get; set; } = 255;
        [GameProperty("9", 255, true)]
        public byte Blue { get; set; } = 255;

        [GameProperty("10", 0.5f, true)]
        public float FadeTime { get; set; } = 0.5f;

        public bool PlayerColor1 { get; set; }
        public bool PlayerColor2 { get; set; }
        public bool Blending { get; set; }
        [GameProperty("35", 1f, true)]
        public float Opacity { get; set; } = 1f;
        public bool CopyOpacity { get; set; }

        public HSV ColorHSV { get; set; }
        public int TargetChannelID { get; set; }

        public ColorTrigger() : base(899)
        {
            IsTrigger = true;
        }

        public ColorTrigger(string[] data)
        {
            for (int i = 0; i < data.Length; i += 2)
                LoadProperty(byte.Parse(data[i]), data[i + 1]);
        }

        public override void LoadProperty(byte key, string value)
        {
            switch (key)
            {
                case 10:
                    FadeTime = int.Parse(value);
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
                case 15:
                    PlayerColor1 = GameConvert.StringToBool(value);
                    return;
                case 16:
                    PlayerColor2 = GameConvert.StringToBool(value);
                    return;
                case 35:
                    Opacity = GameConvert.StringToSingle(value);
                    return;
                case 23:
                    ColorID = short.Parse(value);
                    return;
                case 17:
                    Blending = GameConvert.StringToBool(value);
                    return;
                case 60:
                    CopyOpacity = GameConvert.StringToBool(value);
                    return;
                case 49:
                    ColorHSV = new HSV(value);
                    return;
                case 50:
                    TargetChannelID = short.Parse(value);
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
            builder.Append($",23,{ColorID}");
            builder.Append($",7,{Red}");
            builder.Append($",8,{Green}");
            builder.Append($",9,{Blue}");
            builder.Append($",10,{GameConvert.SingleToString(FadeTime)}");
            builder.Append($",35,{GameConvert.SingleToString(Opacity)}");
            if (PlayerColor1)
                builder.Append($",15,1");
            if (PlayerColor2)
                builder.Append($",16,1");
            if (Blending)
                builder.Append($",17,1");
            if (CopyOpacity)
                builder.Append($",60,1");
            if (TargetChannelID != 0 && ColorHSV != null && !ColorHSV.IsDefault)
                builder.Append($",49,{ColorHSV.ToString()}");
            if (TargetChannelID != 0)
                builder.Append($",50,{TargetChannelID}");
            return builder.ToString();
        }
    }
}
