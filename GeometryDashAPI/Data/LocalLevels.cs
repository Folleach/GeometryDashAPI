using GeometryDashAPI.Data.Enums;
using GeometryDashAPI.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeometryDashAPI.Data
{
    public class LocalLevels : GameData, IEnumerable<LevelCreatorModel>
    {
        private List<LevelCreatorModel> levels { get; set; }
        private Dictionary<string, Dictionary<int, int>> index;

        public int BinaryVersion
        {
            get => DataPlist["LLM_02"];
            set => DataPlist["LLM_02"] = value;
        }

        public int LevelCount => levels.Count;

        public LocalLevels() : base(GameDataType.LocalLevels)
        {
        }

        public LocalLevels(string fileName) : base(fileName)
        {
        }

        private LocalLevels(string fileName, bool preventLoading) : base(fileName, preventLoading)
        {
        }

        public override async Task Load()
        {
            await base.Load();
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

        public void Remove(params LevelCreatorModel[] items)
        {
            foreach (var level in items)
            {
                levels.Remove(level);
                DataPlist["LLM_01"].Remove(level.KeyInDict);
            }
            RecalculateIndex();
        }

        public IEnumerator<LevelCreatorModel> GetEnumerator()
        {
            foreach (var level in levels)
                yield return level;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static async Task<LocalLevels> LoadAsync(string fileName = null)
        {
            var local = new LocalLevels(fileName ?? ResolveFileName(GameDataType.LocalLevels), preventLoading: true);
            await local.Load();
            return local;
        }
    }
}
