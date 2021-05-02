using System.Runtime.CompilerServices;
using GeometryDashAPI.Parser;

[assembly: InternalsVisibleTo("GeometryDashAPI.Tests")]

namespace GeometryDashAPI
{
    public class GeometryDashApi
    {
        private static GeometryDashObjectParser objectParser;
        
        public static GeometryDashObjectParser GetObjectParser()
        {
            return objectParser ?? new GeometryDashObjectParser();
        }
    }
}