using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GeometryDashAPI.Serialization
{
    public class Plist : Dictionary<string, dynamic>
    {
        public Plist()
        {
        }

        public Plist(byte[] bytes)
        {
            this.Load(new MemoryStream(bytes));
        }

        private void Load(MemoryStream stream)
        {
            base.Clear();

            XDocument doc = XDocument.Load(stream);
            XElement plist = doc.Element("plist");
            XElement dict = plist.Element("dict");

            IEnumerable<XElement> dictElements = dict.Elements();
            this.Parse(this, dictElements);
        }

        private void Parse(Plist dict, IEnumerable<XElement> elements)
        {
            for (int i = 0; i < elements.Count(); i += 2)
            {
                XElement key = elements.ElementAt(i);
                XElement value = elements.ElementAt(i + 1);
                dict[key.Value] = ParseValue(value);
            }
        }

        private dynamic ParseValue(XElement val)
        {
            switch (val.Name.ToString())
            {
                case "string":
                case "s":
                    return val.Value;
                case "integer":
                case "i":
                    return int.Parse(val.Value);
                case "real":
                case "r":
                    return float.Parse(val.Value, NumberStyles.Any, Culture.FormatProvider);
                case "true":
                case "t":
                    return true;
                case "false":
                case "f":
                    return false;
                case "dict":
                case "d":
                    Plist plist = new Plist();
                    Parse(plist, val.Elements());
                    return plist;
                default:
                    throw new Exception("Plist type not supported");
            }
        }

        public static string PlistToString(Plist plist)
        {
            string head = "<?xml version=\"1.0\"?><plist version=\"1.0\" gjver=\"2.0\"><dict>";
            string end = "</dict></plist>";
            return $"{head}{PTS(plist)}{end}";
        }

        private static string PTS(Plist plist)
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, dynamic> element in plist)
            {
                builder.Append($"<k>{element.Key}</k>");
                if (element.Value is string)
                    builder.Append($"<s>{element.Value}</s>");
                else if (element.Value is int)
                    builder.Append($"<i>{element.Value}</i>");
                else if (element.Value is float)
                    builder.Append($"<r>{element.Value.ToString().Replace(',', '.')}</r>");
                else if (element.Value is bool)
                {
                    if (element.Value)
                        builder.Append("<t />");
                    else
                        builder.Append("<f />");
                }
                else if (element.Value is Plist)
                {
                    if ((element.Value as Plist).Values.Count == 0)
                    {
                        builder.Append("<d />");
                        continue;
                    }
                    builder.Append($"<d>{PTS(element.Value)}</d>");
                }
            }
            return builder.ToString().Replace("&", "&amp;");
        }
    }
}
