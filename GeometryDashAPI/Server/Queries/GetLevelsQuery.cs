using GeometryDashAPI.Server.Enums;
using GeometryDashAPI.Server.Queries;
using System.Collections.Generic;

namespace GeometryDashAPI.Server
{
    public class GetLevelsQuery : IQuery
    {
        public string QueryString { get; set; } = "-";
        public bool IsOfficialSong { get; private set; }
        public int SongID { get; private set; }
        public SearchType SearchType { get; set; } = SearchType.MostLiked;
        public List<LengthType> Lengths { get; set; } = new List<LengthType>();
        public List<SearchDifficulty> Difficults { get; set; } = new List<SearchDifficulty>();
        public DemonDifficult DemonDifficult { get; set; }
        public int Page { get; set; } = 0;
        public int Total { get; set; } = 0;
        public int Feautured { get; set; } = -1;
        public bool Uncomplited { get; set; } = false;
        public bool Complited { get; set; } = false;
        public bool Original { get; set; } = false;
        public bool Epic { get; set; } = false;
        public bool TwoPlayer { get; set; } = false;
        public bool HasCoins { get; set; } = false;

        public GetLevelsQuery(SearchType searchType)
        {
            SearchType = searchType;
        }

        public void SetSong(int id)
        {
            IsOfficialSong = false;
            SongID = id;
        }

        public void SetSong(OfficialSong song)
        {
            IsOfficialSong = true;
            SongID = (int)song;
        }

        public Parameters BuildQuery()
        {
            Parameters result = new Parameters();
            BuildQuery(result);
            return result;
        }

        public void BuildQuery(Parameters parameters)
        {
            string difficultsString = "";
            if (Difficults.Count > 0)
            {
                foreach (SearchDifficulty diff in Difficults)
                    difficultsString += ((int)diff).ToString();
            }
            else
                difficultsString = "-";

            string lengthsString = "";
            if (Lengths.Count > 0)
            {
                foreach (LengthType len in Lengths)
                    lengthsString += ((int)len).ToString();
            }
            else
                lengthsString = "-";

            parameters.Add(new Property("str", QueryString));
            parameters.Add(new Property("diff", difficultsString));
            parameters.Add(new Property("len", lengthsString));
            parameters.Add(new Property("page", Page));
            parameters.Add(new Property("type", (int)SearchType));
            parameters.Add(new Property("uncomplited", GameConvert.BoolToString(Uncomplited)));
            parameters.Add(new Property("onlyComplited", GameConvert.BoolToString(Complited)));
            parameters.Add(new Property("total", Total));
            parameters.Add(new Property("original", GameConvert.BoolToString(Original)));
            parameters.Add(new Property("twoPlayer", GameConvert.BoolToString(TwoPlayer)));
            parameters.Add(new Property("coins", GameConvert.BoolToString(HasCoins)));
            parameters.Add(new Property("epic", GameConvert.BoolToString(Epic)));

            foreach (SearchDifficulty diff in Difficults)
            {
                if (diff == SearchDifficulty.Demon)
                    parameters.Add(new Property("demonFilter", (int)DemonDifficult));
            }
            if (Feautured >= 0)
                parameters.Add(new Property("featured", Feautured));
        }
    }
}