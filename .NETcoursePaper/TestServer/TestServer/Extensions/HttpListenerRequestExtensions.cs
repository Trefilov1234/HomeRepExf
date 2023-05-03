using System.Net;

namespace TestServer.Extensions
{
    public static class HttpListenerRequestExtensions
    {
        public static bool TryGetQueryInt(this HttpListenerRequest request, string key, out int result)
        {
            return int.TryParse(request.QueryString[key], out result);
        }

        public static bool TryGetQueryBool(this HttpListenerRequest request, string key, out bool result)
        {
            return bool.TryParse(request.QueryString[key], out result);
        }

        public static bool GetQueryBool(this HttpListenerRequest request, string key)
        {
            return bool.TryParse(request.QueryString[key], out var result) && result;
        }

        public static string GetQueryString(this HttpListenerRequest request, string key)
        {
            return request.QueryString[key];
        }
    }
}
