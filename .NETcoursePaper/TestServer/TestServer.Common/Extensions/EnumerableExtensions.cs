﻿using System;
using System.Collections.Generic;

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
