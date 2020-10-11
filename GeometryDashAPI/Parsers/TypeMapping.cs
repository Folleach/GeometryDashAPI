using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Levels.GameObjects.Specific;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace GeometryDashAPI.Parsers
{
    public class TypeMapping : IEnumerable<KeyValuePair<string, Type>>
    {
        private Dictionary<string, Type> intToType = new Dictionary<string, Type>()
        {
            { "1", typeof(BaseBlock) },
            { "8", typeof(BaseBlock) },

            { "1658", typeof(DetailBlock) },
            { "1888", typeof(DetailBlock) },

            { "914", typeof(TextBlock) },
        };

        public ImmutableList<Type> Types { get; }

        public TypeMapping(Dictionary<int, Type> include = null, int[] exclude = null)
        {
            HashSet<Type> addedTypes = new HashSet<Type>();
            ImmutableList<Type>.Builder types = ImmutableList.CreateBuilder<Type>();

            if (include != null)
            {
                // TODO: Do something
            }
            if (exclude != null)
            {
                // TODO: Do something
            }

            foreach (var item in intToType)
            {
                if (addedTypes.Contains(item.Value))
                    continue;
                addedTypes.Add(item.Value);
                types.Add(item.Value);
            }
            Types = types.ToImmutable();
        }

        public Type Get(string id) => intToType[id];

        public IEnumerator<KeyValuePair<string, Type>> GetEnumerator()
        {
            foreach (var item in intToType)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
