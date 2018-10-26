using GeometryDashAPI.Exceptions;
using GeometryDashAPI.Levels.Enums;
using System;
using System.Globalization;
using System.Text;

namespace GeometryDashAPI.Levels
{
    public class Color
    {
        const char Sp = '_';

        public short ID { get; internal set; }
        public byte Red { get; set; } = 255;
        public byte Green { get; set; } = 255;
        public byte Blue { get; set; } = 255;
        public byte Red2 { get; set; } = 255;
        public byte Green2 { get; set; } = 255;
        public byte Blue2 { get; set; } = 255;

        /// <summary>
        /// -1 - none, 0 - none, 1 - PlayerColor1, 2 - PlayerColor2
        /// </summary>
        public sbyte PlayerColor { get; set; }
        public bool Blending { get; set; }
        public float Opacity { get; set; } = 1f;
        public bool CopyOpacity { get; set; }

        public HSV ColorHSV { get; set; }
        public int TargetChannelID { get; set; }

        public Color(short id)
        {
            this.ID = id;
        }
        public Color(ColorType type)
        {
            this.ID = (short)type;
        }
        public Color(short id, byte r, byte g, byte b)
        {
            this.ID = id;
            this.SetColor(r, g, b);
        }
        public Color(ColorType type, byte r, byte g, byte b)
        {
            this.ID = (short)type;
            this.SetColor(r, g, b);
        }
        public Color(string data)
        {
            string[] properties = data.Split('_');
            for (int i = 0; i < properties.Length - 1; i += 2)
            {
                switch (properties[i])
                {
                    case "1":
                        Red = byte.Parse(properties[i + 1]);
                        break;
                    case "2":
                        Green = byte.Parse(properties[i + 1]);
                        break;
                    case "3":
                        Blue = byte.Parse(properties[i + 1]);
                        break;
                    case "11":
                        Red2 = byte.Parse(properties[i + 1]);
                        break;
                    case "12":
                        Green2 = byte.Parse(properties[i + 1]);
                        break;
                    case "13":
                        Blue2 = byte.Parse(properties[i + 1]);
                        break;
                    case "4":
                        PlayerColor = sbyte.Parse(properties[i + 1]);
                        break;
                    case "5":
                        Blending = GameConvert.StringToBool(properties[i + 1], false);
                        break;
                    case "6":
                        ID = short.Parse(properties[i + 1]);
                        break;
                    case "7":
                        Opacity = float.Parse(properties[i + 1], NumberStyles.Any, Culture.FormatProvider);
                        break;
                    case "9":
                        TargetChannelID = int.Parse(properties[i + 1]);
                        break;
                    case "10":
                        ColorHSV = new HSV(properties[i + 1]);
                        break;
                    case "15":
                        //TODO: Added to class
                        break;
                    case "17":
                        CopyOpacity = GameConvert.StringToBool(properties[i + 1]);
                        break;
                    case "18":
                        //TODO: Added to class
                        break;
                    case "8":
                        //TODO: Added to class
                        break;
                    default:
                        throw new PropertyNotSupportedException(properties[i], properties[i + 1]);
                }
            }
        }

        public void SetColor(byte r, byte g, byte b)
        {
            Red = r;
            Green = g;
            Blue = b;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"1{Sp}{Red}{Sp}" +
                $"2{Sp}{Green}{Sp}" +
                $"3{Sp}{Blue}{Sp}" +
                $"11{Sp}{Red2}{Sp}" +
                $"12{Sp}{Green2}{Sp}" +
                $"13{Sp}{Blue2}{Sp}" +
                $"6{Sp}{ID}{Sp}" +
                $"7{Sp}{GameConvert.SingleToString(Opacity)}{Sp}" +
                $"15{Sp}1{Sp}18{Sp}0{Sp}8{Sp}1");

            if (PlayerColor != -1 && PlayerColor != 0)
                builder.Append($"{Sp}4{Sp}{PlayerColor}");
            if (Blending)
                builder.Append($"{Sp}5{Sp}1");

            if (TargetChannelID != 0)
            {
                builder.Append($"{Sp}9{Sp}{TargetChannelID}{Sp}10{Sp}{(ColorHSV == null ? new HSV().ToString() : ColorHSV.ToString())}");
                if (CopyOpacity)
                    builder.Append($"{Sp}17{Sp}1");
            }
            return builder.ToString();
        }
    }
}
