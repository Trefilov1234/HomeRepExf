using System.Net;
using System.Text.RegularExpressions;

namespace TestServer.Extensions
{
    public static class MatchExtensions
    {
        public static int GetIntGroup(this Match match, string groupKey)
        {
            return int.Parse(match.Groups[groupKey].Value);
        }
    }
}
