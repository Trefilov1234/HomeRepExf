using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditTestClient.Api.Requests
{
    public class UserRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }
}
