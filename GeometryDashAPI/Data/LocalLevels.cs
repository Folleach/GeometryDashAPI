using GeometryDashAPI.Data.Enums;
using GeometryDashAPI.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeometryDashAPI.Serialization;

namespace GeometryDashAPI.Data
{
    public class LocalLevels : GameData, IReadOnlyCollection<LevelCreatorModel>
    {
        private List<LevelCreatorModel> levels { get; set; }
        private Dictionary<string, Dictionary<int, int>> index;

        public int BinaryVersion
        {
            get => DataPlist["LLM_02"];
            set => DataPlist["LLM_02"] = value;
        }

        [Obsolete("Use Count instead", true)]
        public int LevelCount => levels.Count;

        protected LocalLevels() : base(GameDataType.LocalLevels)
        {
        }

        public override async Task LoadAsync(string fileName)
        {
            await base.LoadAsync(fileName);
            LoadLevels();
        }

        private void LoadLevels()
        {
            levels = new List<LevelCreatorModel>();
            foreach (var a in DataPlist["LLM_01"])
            {
                if (a.Key == "_isArr")
                    continue;
                var level = new LevelCreatorModel(a.Key, a.Value);
                levels.Add(level);
            }
            RecalculateIndex();
        }

        private void RecalculateIndex()
        {
            if (index == null)
                index = new Dictionary<string, Dictionary<int, int>>();
            else
                index.Clear();
            
            for (var i = 0; i < levels.Count; ++i)
            {
                var level = levels[i];
                var revisionIndexMap = index.GetOrCreate(level.Name, _ => new Dictionary<int, int>());
                if (!revisionIndexMap.ContainsKey(level.Revision))
                    revisionIndexMap.Add(level.Revision, i);
            }
        }

        [Obsolete("Use the instance.GetLevel(name, revision)")]
        public LevelCreatorModel GetLevelByName(string levelName)
        {
            return levels.Find(x => x.Name == levelName);
        }

        public LevelCreatorModel GetLevel(string levelName, int revision = 0)
        {
            return levels[index[levelName][revision]];
        }

        public LevelCreatorModel GetLevel(int index)
        {
            return levels[index];
        }

        public List<LevelCreatorModel> GetAllLevelsByName(string levelName)
        {
            return this.levels.FindAll(x => x.Name == levelName);
        }

        [Obsolete("Use the instance.LevelExists(name, revision)")]
        public bool LevelExist(string levelName)
        {
            return this.levels.Exists(x => x.Name == levelName);
        }

        public bool LevelExists(string levelName, int revision = 0)
        {
            return index.ContainsKey(levelName) && index[levelName].ContainsKey(revision);
        }

        public bool Remove(LevelCreatorModel levelInfo)
        {
            if (levelInfo.KeyInDict == null)
                return false;
            if (!levels.Contains(levelInfo))
                return false;
            levels.Remove(levelInfo);
            DataPlist["LLM_01"].Remove(levelInfo.KeyInDict);
            RecalculateIndex();
            return true;
        }

        public int Count => levels.Count;

        public IEnumerator<LevelCreatorModel> GetEnumerator()
        {
            foreach (var level in levels)
                yield return level;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static async Task<LocalLevels> LoadFileAsync(string? fileName = null)
        {
            var local = new LocalLevels();
            await local.LoadAsync(fileName ?? ResolveFileName(GameDataType.LocalLevels));
            return local;
        }

        public static LocalLevels LoadFile(string? fileName = null)
        {
            var local = new LocalLevels();
            local.LoadAsync(fileName ?? ResolveFileName(GameDataType.LocalLevels)).GetAwaiter().GetResult();
            return local;
        }

        /// <summary>
        /// Creates a empty local levels instance, like Geometry Dash does in first time start
        /// </summary>
        public static LocalLevels CreateNew()
        {
            var local = new LocalLevels
            {
                DataPlist = new Plist()
                {
                    ["LLM_02"] = GeometryDashApi.BinaryVersion,
                    ["LLM_01"] = new Plist()
                    {
                        ["_isArr"] = true
                    }
                }
            };
            local.LoadLevels();
            return local;
        }

        public void AddLevel(LevelCreatorModel levelInfo)
        {
            var all = DataPlist["LLM_01"];
            for (var i = LevelCount - 1; i >= 0; i--)
                all[$"k_{i + 1}"] = all[$"k_{i}"];
            all["k_0"] = levelInfo.DataLevel;
            LoadLevels();
        }
    }
}
