using GeometryDashAPI.Server.Enums;
using GeometryDashAPI.Server.Models;
using System;
using System.Collections.Generic;

namespace GeometryDashAPI.Server
{
    public class GetLevelsQuery {
        #region fields
        public string String { get; set; } = "-";
        public OfficialSong OfficialSong
        {
            get => OfficialSong;
            set
            {
                CustomSongID = 0;
                OfficialSong = value;
            }
        }
        public SearchType SearchType { get; set; } = SearchType.MostLiked;
        public List<LengthType> Lengths { get; set; } = new List<LengthType>();
        public List<Difficult> Difficults { get; set; } = new List<Difficult>();
        public DemonDifficult DemonDifficult { get; set; }
        public int CustomSongID
        {
            get => CustomSongID;
            set
            {
                OfficialSong = OfficialSong.NotChoosen;
                CustomSongID = value;
            }
        }
        public int Page { get; set; } = 0;
        public int Total { get; set; } = 11;
        public int Feautured { get; set; } = -1;
        public bool Uncomplited { get; set; } = false;
        public bool Complited { get; set; } = false;
        public bool Original { get; set; } = false;
        public bool Epic { get; set; } = false;
        public bool TwoPlayer { get; set; } = false;
        public bool HasCoins { get; set; } = false;
        #endregion

        List<Property> Properties = new List<Property>();

        int BoolToNum(bool value)
        {
            return value ? 1 : 0;
        }

        public GetLevelsQuery(SearchType searchType)
        {
            SearchType = searchType;
        }

        public List<Property> BuildQuery()
        {
            string difficultsString = "";
            if (Difficults.Count > 0)
                foreach (Difficult diff in Difficults)
                    difficultsString += ((int)diff).ToString();
            else
                difficultsString = "-";

            string lengthsString = "";
            if (Lengths.Count > 0)
                foreach (LengthType len in Lengths)
                    lengthsString+=((int)len).ToString();
            else
                lengthsString = "-";

            Properties.Add(new Property("str", String));
            Properties.Add(new Property("diff", difficultsString));
            Properties.Add(new Property("len", lengthsString));
            Properties.Add(new Property("page", Page));
            Properties.Add(new Property("type", (int)SearchType));
            Properties.Add(new Property("uncomplited", BoolToNum(Uncomplited)));
            Properties.Add(new Property("onlyComplited", BoolToNum(Complited)));
            Properties.Add(new Property("total", Total));
            Properties.Add(new Property("original", BoolToNum(Original)));
            Properties.Add(new Property("twoPlayer", BoolToNum(TwoPlayer)));
            Properties.Add(new Property("coins", BoolToNum(HasCoins)));
            Properties.Add(new Property("epic", BoolToNum(Epic)));

            foreach (Difficult diff in Difficults)
                if (diff == Difficult.Demon)
                    Properties.Add(new Property("demonFilter", (int)DemonDifficult));
            
            if(Feautured>=0)
                Properties.Add(new Property("featured", Feautured));

            return Properties;
        }
    }
}