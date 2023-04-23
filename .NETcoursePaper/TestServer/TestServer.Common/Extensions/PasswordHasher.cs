using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestServer.Common.Extensions
{
    public static class PasswordHasher
    {
       
        public static byte[] GenerateSha256Hash(string password)
        {
            var data = Encoding.ASCII.GetBytes(password);
            var md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(data);

        }
    }
}
