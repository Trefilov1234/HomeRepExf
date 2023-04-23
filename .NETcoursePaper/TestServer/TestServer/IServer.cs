using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer
{
    public interface IServer:IDisposable
    {
        public Task StartAsync(string uri);
    }
}
