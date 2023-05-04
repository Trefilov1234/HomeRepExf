using System;
using System.Threading.Tasks;

namespace TestServer
{
    public interface IServer : IDisposable
    {
        public Task StartAsync(string uri);
    }
}
