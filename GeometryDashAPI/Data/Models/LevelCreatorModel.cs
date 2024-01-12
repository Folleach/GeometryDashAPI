using GeometryDashAPI.Levels;
using System;
using System.Text;
using GeometryDashAPI.Serialization;

namespace GeometryDashAPI.Data.Models
{
    public class LevelCreatorModel
    {
        internal string? KeyInDict { get; private set; }
        public Plist DataLevel { get; set; }

        public string? Name
        {
            get => DataLevel.TryGetValue("k2", out var name) ? name : null;
            set => DataLevel["k2"] = value!;
        }

        public string Description
        {
            get => DataLevel.ContainsKey("k3") ? Encoding.ASCII.GetString(Convert.FromBase64String(DataLevel["k3"])) : "";
            set => DataLevel["k3"] = Convert.ToBase64String(Encoding.ASCII.GetBytes(value));
        }

        public string LevelString => DataLevel.ContainsKey("k4") ? DataLevel["k4"] : Level.DefaultLevelString;

        public string? AuthorName
        {
            get => DataLevel.TryGetValue("k5", out var name) ? name : null;
            set => DataLevel["k5"] = value!;
        }

        public bool Verified
        {
            get => DataLevel.ContainsKey("k14") ? DataLevel["k14"] : false;
            set => DataLevel["k14"] = value;
        }
        public int Version
        {
            get => DataLevel["k16"];
            set => DataLevel["k16"] = value;
        }
        public int TotalAttempts
        {
            get => DataLevel.ContainsKey("k18") ? DataLevel["k18"] : 0;
            set => DataLevel["k18"] = value;
        }
        public int NormalProgress
        {
            get => DataLevel.ContainsKey("k19") ? DataLevel["k19"] : 0;
            set
            {
                //This is weird. May need to be redone
                DataLevel["k19"] = value;
                DataLevel["k71"] = value;
                DataLevel["k90"] = value;
            }
        }
        public int PracticeProgress
        {
            get => DataLevel.ContainsKey("k20") ? DataLevel["k20"] : 0;
            set => DataLevel["k20"] = value;
        }
        public int TotalJumps
        {
            get => DataLevel.ContainsKey("k36") ? DataLevel["k36"] : 0;
            set => DataLevel["k36"] = value;
        }
        public int Revision
        {
            get => DataLevel.ContainsKey("k46") ? DataLevel["k46"] : 0;
            set => DataLevel["k46"] = value;
        }
        public int CountObject
        {
            get => DataLevel.ContainsKey("k48") ? DataLevel["k48"] : 0;
            set => DataLevel["k48"] = value;
        }
        public int BinaryVersion
        {
            get => DataLevel["k50"];
            set => DataLevel["k50"] = value;
        }
        public bool CollectCoin1
        {
            get => DataLevel.ContainsKey("k61") ? DataLevel["k61"] : false;
            set => DataLevel["k61"] = value;
        }
        public bool CollectCoin2
        {
            get => DataLevel.ContainsKey("k62") ? DataLevel["k62"] : false;
            set => DataLevel["k62"] = value;
        }
        public bool CollectCoin3
        {
            get => DataLevel.ContainsKey("k63") ? DataLevel["k63"] : false;
            set => DataLevel["k63"] = value;
        }
        public bool CustomMusic
        {
            get => DataLevel.ContainsKey("k45");
        }
        public int MusicId
        {
            get => CustomMusic ? DataLevel["k45"] : DataLevel.ContainsKey("k8") ? DataLevel ["k8"] : 0;
        }

        public float CameraPositionX
        {
            get => DataLevel.ContainsKey("kI1") ? DataLevel["kI1"] : 0;
            set => DataLevel["kI1"] = value;
        }
        public float CameraPositionY
        {
            get => DataLevel.ContainsKey("kI2") ? DataLevel["kI2"] : 0;
            set => DataLevel["kI2"] = value;
        }

        public void SetMusicId(bool custom, int id)
        {
            if (custom)
                DataLevel["k45"] = id;
            else
            {
                DataLevel["k8"] = id;
                DataLevel.Remove("k45");
            }
        }

        public LevelCreatorModel(string? key, Plist dict)
        {
            KeyInDict = key;
            DataLevel = dict;
        }

        public Level LoadLevel() => new(LevelString, compressed: true);
        public void SaveLevel(Level level) => DataLevel["k4"] = level.SaveAsString();

        public override string ToString()
        {
            return $"{Name}";
        }

        /// <summary>
        /// Creates a empty local levels instance, like Geometry Dash does in first time start
        /// </summary>
        public static LevelCreatorModel CreateNew(string name, string authorName, Level level)
        {
            var model = new LevelCreatorModel(null, new Plist
            {
                ["kCEK"] = 4,
                ["k13"] = true,
                ["k21"] = 2,
                ["kI1"] = 0,
                ["kI2"] = 0,
                ["kI3"] = 0,
                ["kI6"] = new Plist()
            })
            {
                Name = name,
                AuthorName = authorName,
                Version = 1,
                BinaryVersion = GeometryDashApi.BinaryVersion
            };

            model.SaveLevel(level);

            return model;
        }

        /// <summary>
        /// Creates a empty local levels instance, like Geometry Dash does in first time start
        /// </summary>
        public static LevelCreatorModel CreateNew(string name, string authorName)
            => CreateNew(name, authorName, new Level());

        /// <summary>
        /// Creates a empty local levels instance, like Geometry Dash does in first time start
        /// </summary>
        public static LevelCreatorModel CreateNew(string name, GameManager manager)
            => CreateNew(name, manager.PlayerName);

        /// <summary>
        /// Creates a empty local levels instance, like Geometry Dash does in first time start
        /// </summary>
        public static LevelCreatorModel CreateNew(string name, GameManager manager, Level level)
            => CreateNew(name, manager.PlayerName, level);
    }
}
