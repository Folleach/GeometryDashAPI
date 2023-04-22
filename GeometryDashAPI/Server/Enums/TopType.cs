using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Server.Enums
{
    public enum TopType
    {
        [OriginalName("top")] Top,
        [OriginalName("friends")] Friends,
        [OriginalName("relative")] Global,
        [OriginalName("creators")] Creators
    }
}
