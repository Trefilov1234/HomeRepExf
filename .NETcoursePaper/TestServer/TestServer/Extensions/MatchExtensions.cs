using System.Net;
using System.Text.RegularExpressions;

namespace TestServer.Extensions
{
    public static class MatchExtensions
    {
        public static int GetIntGroup(this string path, HttpListenerContext context, string group)
        {
            var match = Regex.Match(context.Request.Url.AbsolutePath, path, RegexOptions.IgnoreCase);
            var id = int.Parse(match.Groups[group].Value);
            return id;
        }
    }
}
