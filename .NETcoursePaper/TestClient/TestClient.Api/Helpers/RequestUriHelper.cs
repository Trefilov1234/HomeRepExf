using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient.Api.Helpers
{
    internal class RequestUriHelper
    {
        public static string GetUriWithQueryString(string uri, Dictionary<string, string> parameters = null)
        {
            if (parameters?.Any() != true)
                return uri;

            var isFirstParameter = true;
            var sb = new StringBuilder(uri);
            foreach (var parameter in parameters.Where(parameter => parameter.Value != null))
            {
                sb.Append(isFirstParameter ? '?' : '&');
                sb.Append(parameter.Key);
                sb.Append('=');
                sb.Append(parameter.Value);
                isFirstParameter = false;
            }

            return sb.ToString();
        }
    }
}
