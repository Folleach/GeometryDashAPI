using System.Text.RegularExpressions;

namespace GeometryDashAPI.Server;

internal class ServerResponseHelper
{
    internal static readonly Regex StatusCodeRegex = new(@"^(-|)\d+$");
    internal static readonly Regex ErrorCodeRegex = new(@"^error\scode:\s\d+$");
}
