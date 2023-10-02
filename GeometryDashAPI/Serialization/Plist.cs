using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            foreach (var item in elements.Pairs())
                dict[item.Key.Value] = ParseValue(item.Value);
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

        public string SaveToString()
        {
            var document = new XDocument();
            var root = new XElement("plist");
            root.Add(new XAttribute("version", "1.0"));
            root.Add(new XAttribute("gjver", "2.0"));
            root.Add(RecursivePlistToString(this, "dict"));
            document.Add(root);
            using var memory = new MemoryStream();
            SaveToStream(memory);
            return Encoding.UTF8.GetString(memory.ToArray());
        }

        public void SaveToStream(Stream stream)
        {
            var document = CreateDocumentFromThis();
            document.Save(stream, SaveOptions.DisableFormatting);
        }

        public async Task SaveToStreamAsync(Stream stream)
        {
            var document = CreateDocumentFromThis();
#if NETSTANDARD2_1
            await document.SaveAsync(stream, SaveOptions.DisableFormatting, CancellationToken.None);
#else
            await Task.Run(() => document.Save(stream, SaveOptions.DisableFormatting));
#endif
        }

        private XDocument CreateDocumentFromThis()
        {
            var document = new XDocument();
            var root = new XElement("plist");
            root.Add(new XAttribute("version", "1.0"));
            root.Add(new XAttribute("gjver", "2.0"));
            root.Add(RecursivePlistToString(this, "dict"));
            document.Add(root);
            return document;
        }

        private static XElement RecursivePlistToString(Plist plist, string dictName)
        {
            var dict = new XElement(dictName);
            foreach (var element in plist)
            {
                dict.Add(new XElement("k", element.Key));
                if (element.Value is string)
                    dict.Add(new XElement("s", element.Value));
                else if (element.Value is int)
                    dict.Add(new XElement("i", element.Value));
                else if (element.Value is float)
                    dict.Add(new XElement("r", GameConvert.SingleToString(element.Value)));
                else if (element.Value is bool)
                    dict.Add(new XElement(element.Value ? "t" : "f"));
                else if (element.Value is Plist value)
                {
                    if (value.Values.Count == 0)
                    {
                        dict.Add(new XElement("d"));
                        continue;
                    }
                    dict.Add(RecursivePlistToString(element.Value, "d"));
                }
            }
            return dict;
        }
    }
}
