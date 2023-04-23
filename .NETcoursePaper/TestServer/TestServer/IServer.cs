using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer
{
    public interface IServer
    {
        public Task StartAsync(string uri);
    }
}
