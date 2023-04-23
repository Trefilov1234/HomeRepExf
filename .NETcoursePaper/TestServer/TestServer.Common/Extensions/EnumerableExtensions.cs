using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<TValue>(this IEnumerable<TValue> values, Action<TValue> action)
        {
            foreach (var value in values)
                action(value);
        }
    }
}
