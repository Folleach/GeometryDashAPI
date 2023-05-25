using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace GeometryDashAPI.Serialization
{
    public class Plist : Dictionary<string, dynamic>
    {
        private static readonly XmlReaderSettings xmlSettings = new XmlReaderSettings()
        {
            DtdProcessing = DtdProcessing.Ignore,
            XmlResolver = null
        };

        public Plist()
        {
        }

        public Plist(byte[] bytes) : this(new MemoryStream(bytes))
        {
        }

        public Plist(Stream stream)
        {
            Load(stream);
        }

        private void Load(Stream stream)
        {
            Clear();

            var document = XDocument.Load(XmlReader.Create(stream, xmlSettings));
            var plist = document.Element("plist");
            if (plist == null)
                throw new InvalidOperationException("'plist' element is not found in the xml file");
            var dict = plist.Element("dict");
            if (dict == null)
                throw new InvalidOperationException("'dict' element is not found in the xml file");

            var dictElements = dict.Elements();
            Parse(this, dictElements);
        }

        private void Parse(Plist dict, IEnumerable<XElement> elements)
        {
            foreach (var (key, value) in elements.Pairs())
                dict[key.Value] = ParseValue(value);
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
                    var plist = new Plist();
                    Parse(plist, val.Elements());
                    return plist;
                default:
                    throw new Exception("Plist type not supported");
            }
        }

        public static string PlistToString(Plist plist)
        {
            var head = "<?xml version=\"1.0\"?><plist version=\"1.0\" gjver=\"2.0\"><dict>";
            var end = "</dict></plist>";
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
                    builder.Append(element.Value ? "<t />" : "<f />");
                else if (element.Value is Plist value)
                {
                    if (value.Values.Count == 0)
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
