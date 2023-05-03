using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace TestServer
{
    public interface ICommand
    {
        public string Path { get; }
        public HttpMethod Method { get; }
        public Task HandleRequestAsync(HttpListenerContext context);
    }
}
