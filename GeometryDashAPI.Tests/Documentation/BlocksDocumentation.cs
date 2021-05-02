using GeometryDashAPI;
using GeometryDashAPI.Levels.GameObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GeometryDashAPI.Tests.Documentation
{
    public class BlocksDocumentation
    {
        private static readonly Type ObjectType = typeof(object);
        private static readonly string LineBreak = new string('-', 16);

        public bool IncludeBaseTypes = false;
        public bool TitleIsFullName = false;
        public bool ShowProperties = true;

        public void WriteAllSupportedBlockTo(Assembly fromAssembly, TextWriter writer)
        {
            Type blockInterface = typeof(IBlock);
            var blocks = fromAssembly.GetTypes().Where(type => type.GetInterfaces().Contains(blockInterface));
            foreach (var item in blocks)
            {
                writer.WriteLine(TitleIsFullName ? item.FullName : item.Name);
                if (ShowProperties)
                {
                    writer.WriteLine($"\tid\tdefault\talways\tname");
                    PropertyWrite(item, writer);
                }
            }
        }

        public void PropertyWrite(Type type, TextWriter writer)
        {
            PropertyInfo[] properties = type.GetProperties();
            if (!IncludeBaseTypes)
                properties = properties.Where(x => x.DeclaringType == type).ToArray();
            foreach (var property in properties)
            {
                GamePropertyAttribute defaultProperty = property.GetCustomAttribute<GamePropertyAttribute>();
                if (defaultProperty != null)
                    writer.Write($"\t{defaultProperty.Key}\t{defaultProperty.DefaultValue?.ToString() ?? "null"}\t{defaultProperty.AlwaysSet}");
                else
                    writer.Write($"ALERT!\t\t\t");
                writer.WriteLine($"\t{property.Name}");
            }
            writer.WriteLine(LineBreak);
        }
    }
}