using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Levels
{
    public class Guidelines : List<Guideline>
    {
        const string SEPERATOR = "~";

        public static string Parse(Guidelines guidelines)
        {
            StringBuilder builder = new();
            guidelines.ForEach(x => builder.Append(GameConvert.DoubleToString(x.Timestamp)).Append(SEPERATOR).Append(GetStringFromGuidelineColor(x.Color)).Append(SEPERATOR));
            return builder.ToString();
        }

        public static Guidelines Parse(ReadOnlySpan<char> raw)
        {
            var guidelines = new Guidelines();

            ReadOnlySpan<char> value;
            var parser = new LLParserSpan(SEPERATOR, raw);

            int index = 0;
            double timestamp = 0;
            while ((value = parser.Next()) != null)
            {
                index++;

                if (index % 2 == 0)
                {
                    guidelines.Add(new()
                    {
                        Timestamp = timestamp,
                        Color = GetGuidelineColorFromString(value)
                    });
                }
                else
                    timestamp = GameConvert.StringToDouble(value);
            }

            return guidelines;
        }

        public static string GetStringFromGuidelineColor(GuidelineColor color)
        {
            return color switch
            {
                GuidelineColor.Orange => "0.8",
                GuidelineColor.Yellow => "0.9",
                GuidelineColor.Green => "1",
                _ => throw new IndexOutOfRangeException("undefined color")
            };
        }

        public static GuidelineColor GetGuidelineColorFromString(ReadOnlySpan<char> color)
        {
            return
                color.CompareTo("0", StringComparison.Ordinal) == 0 ? GuidelineColor.Orange :
                color.CompareTo("0.8", StringComparison.Ordinal) == 0 ? GuidelineColor.Orange :
                color.CompareTo("0.9", StringComparison.Ordinal) == 0 ? GuidelineColor.Yellow :
                color.CompareTo("1", StringComparison.Ordinal) == 0 ? GuidelineColor.Green : throw new IndexOutOfRangeException("unknown color");
        }
    }
}
