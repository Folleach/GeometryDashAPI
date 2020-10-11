using GeometryDashAPI.Exceptions;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Parsers;
using System.Globalization;
using System.Text;

namespace GeometryDashAPI.Levels
{
    public class Color
    {
        const char Sp = '_';

        [GameProperty("6", 0, true)] public short ID { get; internal set; }
        [GameProperty("1", 255, true)] public byte Red { get; set; } = 255;
        [GameProperty("2", 255, true)] public byte Green { get; set; } = 255;
        [GameProperty("3", 255, true)] public byte Blue { get; set; } = 255;
        [GameProperty("11", 255, true)] public byte Red2 { get; set; } = 255;
        [GameProperty("12", 255, true)] public byte Green2 { get; set; } = 255;
        [GameProperty("13", 255, true)] public byte Blue2 { get; set; } = 255;

        /// <summary>
        /// -1 - none, 0 - none, 1 - PlayerColor1, 2 - PlayerColor2
        /// </summary>
        [GameProperty("4", 0, false)] public sbyte PlayerColor { get; set; }
        [GameProperty("5", false, false)] public bool Blending { get; set; }
        [GameProperty("7", 1f, true)] public float Opacity { get; set; } = 1f;
        [GameProperty("17", false, false)] public bool CopyOpacity { get; set; }

        [GameProperty("10", null, true)] public HSV ColorHSV { get; set; }
        [GameProperty("9", 0, false)] public int TargetChannelID { get; set; }
        
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
            KeyValueSLLParser flow = new KeyValueSLLParser(Sp, data);
            while (flow.Next())
            {
                switch (flow.Key)
                {
                    case "1":
                        Red = byte.Parse(flow.Value);
                        break;
                    case "2":
                        Green = byte.Parse(flow.Value);
                        break;
                    case "3":
                        Blue = byte.Parse(flow.Value);
                        break;
                    case "11":
                        Red2 = byte.Parse(flow.Value);
                        break;
                    case "12":
                        Green2 = byte.Parse(flow.Value);
                        break;
                    case "13":
                        Blue2 = byte.Parse(flow.Value);
                        break;
                    case "4":
                        PlayerColor = sbyte.Parse(flow.Value);
                        break;
                    case "5":
                        Blending = GameConvert.StringToBool(flow.Value, false);
                        break;
                    case "6":
                        ID = short.Parse(flow.Value);
                        break;
                    case "7":
                        Opacity = float.Parse(flow.Value, NumberStyles.Any, Culture.FormatProvider);
                        break;
                    case "9":
                        TargetChannelID = int.Parse(flow.Value);
                        break;
                    case "10":
                        ColorHSV = new HSV(flow.Value);
                        break;
                    case "15":
                        //TODO: Added to class
                        break;
                    case "17":
                        CopyOpacity = GameConvert.StringToBool(flow.Value);
                        break;
                    case "18":
                        //TODO: Added to class
                        break;
                    case "8":
                        //TODO: Added to class
                        break;
                    default:
                        throw new PropertyNotSupportedException(flow.Key, flow.Value);
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
