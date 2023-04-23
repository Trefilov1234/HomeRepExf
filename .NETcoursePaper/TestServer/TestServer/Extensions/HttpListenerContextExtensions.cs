using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestServer.Extensions
{
    public static class HttpListenerContextExtensions
    {
        public static async Task<string> GetRequestBodyAsync(this HttpListenerContext context, Encoding encoding = null)
        {
            using var streamReader = new StreamReader(context.Request.InputStream, encoding ?? Encoding.UTF8);
            return await streamReader.ReadToEndAsync().ConfigureAwait(false);
        }

        public static async Task WriteResponseAsync(this HttpListenerContext context, int statusCode, string value = null)
        {
            context.Response.StatusCode = statusCode;
            using var streamWriter = new StreamWriter(context.Response.OutputStream, Encoding.UTF8);
            await streamWriter.WriteAsync(value).ConfigureAwait(false);
        }
    }
}
