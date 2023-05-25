using System;
using System.Collections.Generic;
using System.Reflection;

namespace GeometryDashAPI
{
    public static class Extensions
    {
        public static T GetAttributeOfSelected<T>(this Enum value) where T : Attribute
        {
            var info = value.GetType().GetMember(value.ToString())[0];
            var attributes = info.GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? (T)attributes[0] : null;
        }

        public static Type GetMemberType(this MemberInfo member)
        {
            return member switch
            {
                PropertyInfo info => info.PropertyType,
                FieldInfo info => info.FieldType,
                _ => throw new ArgumentException("Not supported member type", nameof(member))
            };
        }

        public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> create)
        {
            if (!dictionary.TryGetValue(key, out var value))
                dictionary.Add(key, value = create(key));
            return value;
        }

        public static GameType GetGameType(this OfficialLevel level)
        {
            return level switch
            {
                OfficialLevel.StereoMadness => GameType.Default,
                OfficialLevel.BackOnTrack => GameType.Default,
                OfficialLevel.Polargeist => GameType.Default,
                OfficialLevel.DryOut => GameType.Default,
                OfficialLevel.BaseAfterBase => GameType.Default,
                OfficialLevel.CantLetGo => GameType.Default,
                OfficialLevel.Jumper => GameType.Default,
                OfficialLevel.TimeMachine => GameType.Default,
                OfficialLevel.Cycles => GameType.Default,
                OfficialLevel.xStep => GameType.Default,
                OfficialLevel.Clutterfunk => GameType.Default,
                OfficialLevel.TheoryOfEverything => GameType.Default,
                OfficialLevel.ElectromanAdventures => GameType.Default,
                OfficialLevel.Clubstep => GameType.Default,
                OfficialLevel.Electrodynamix => GameType.Default,
                OfficialLevel.HexagonForce => GameType.Default,
                OfficialLevel.BlastProcessing => GameType.Default,
                OfficialLevel.TheoryOfEverything2 => GameType.Default,
                OfficialLevel.GeometricalDominator => GameType.Default,
                OfficialLevel.Deadlocked => GameType.Default,
                OfficialLevel.Fingerdash => GameType.Default,
                OfficialLevel.TheChallenge => GameType.Default,

                OfficialLevel.TheSevenSeas => GameType.Meltdown,
                OfficialLevel.VikingArena => GameType.Meltdown,
                OfficialLevel.AirborneRobots => GameType.Meltdown,

                OfficialLevel.Payload => GameType.World,
                OfficialLevel.BeastMode => GameType.World,
                OfficialLevel.Machina => GameType.World,
                OfficialLevel.Years => GameType.World,
                OfficialLevel.Frontlines => GameType.World,
                OfficialLevel.SpacePirates => GameType.World,
                OfficialLevel.Striker => GameType.World,
                OfficialLevel.Embers => GameType.World,
                OfficialLevel.Round1 => GameType.World,
                OfficialLevel.MonsterDanceOff => GameType.World,
                _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
            };
        }

        internal static IEnumerable<KeyValuePair<T, T>> Pairs<T>(this IEnumerable<T> source)
        {
            var first = false;
            T firstItem = default;
            foreach (var item in source)
            {
                first = !first;
                if (first)
                    firstItem = item;
                else
                    yield return new KeyValuePair<T, T>(firstItem, item);
            }

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (first)
                yield return new KeyValuePair<T, T>(firstItem, default);
        }
    }
}
