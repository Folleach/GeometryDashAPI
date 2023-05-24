using System;
using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Server.Dtos
{
    [Sense("~|~")]
    public class MusicInfo : GameObject
    {
        [GameProperty("1")] public int MusicId { get; set; }
        [GameProperty("2")] public string MusicName { get; set; }
        [GameProperty("4")] public string AuthorName { get; set; }
        [GameProperty("5")] public double SizeInMb { get; set; }
        [GameProperty("10")] private string url;
        public string Url
        {
            get => Uri.UnescapeDataString(url);
            set => url = Uri.EscapeDataString(value);
        }
    }
}
