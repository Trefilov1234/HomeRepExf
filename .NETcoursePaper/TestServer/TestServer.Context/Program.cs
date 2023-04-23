using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServer.Context.Entities;

namespace TestServer.Context
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using(var db=new TestContext())
            {
                //db.Tests.Add(new Test() { Name = "test1", AttemptsCount = 5 });
                //db.SaveChanges();
                var tests=db.Tests;
                foreach (var test in tests)
                {
                    Console.WriteLine($"{test.Id} {test.Name} {test.AttemptsCount}");
                }
            }
        }
    }
}
