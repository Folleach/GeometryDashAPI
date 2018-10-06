using GeometryDashAPI.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Data.Models
{
    public class LevelCreatorModel
    {
        public Dictionary<string, dynamic> DataLevel { get; protected set; }
        internal string KeyInDict { get; private set; }

        public string Name
        {
            get => DataLevel["k2"];
            set => DataLevel["k2"] = value;
        }
        public string Description
        {
            get => DataLevel.ContainsKey("k3") ? Encoding.ASCII.GetString(Convert.FromBase64String(DataLevel["k3"])) : "";
            set => DataLevel["k3"] = Convert.ToBase64String(Encoding.ASCII.GetBytes(value));
        }
        public string LevelString
        {
            get => DataLevel.ContainsKey("k4") ? DataLevel ["k4"] : Level.DefaultLevelString;
            set => DataLevel["k4"] = value;
        }
        public string AuthorName
        {
            get => DataLevel["k5"];
            set => DataLevel["k5"] = value;
        }
        public bool Verified
        {
            get => DataLevel.ContainsKey("k14") ? DataLevel["k14"] : false;
            set => DataLevel["k14"] = value;
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
        public int CountObject
        {
            get => DataLevel.ContainsKey("k48") ? DataLevel["k48"] : 0;
            set => DataLevel["k48"] = value;
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

        public LevelCreatorModel(string key, Dictionary<string, dynamic> dict)
        {
            this.KeyInDict = key;
            this.DataLevel = dict;
        }
    }
}
