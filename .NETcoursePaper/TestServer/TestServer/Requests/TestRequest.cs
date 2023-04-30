using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer.Requests
{
    public class TestRequest
    {
        public string Name { get;set; }
        public int AttemptsCount { get;set; }
        public int CreatedBy { get; set; }
    }
}
