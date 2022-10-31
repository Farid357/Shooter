using System;
using System.Collections.Generic;
using System.Linq;

namespace Shooter.Tools
{
    public static class EnumerableUtils
    {
        public static bool ContainsElementFrom<T>(this List<T> list, IEnumerable<T> enumerable)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();
            var count = array.Count() > list.Count ? array.Count() : list.Count;

            for (var i = 0; i < count; i++)
            {
                if (list.Contains(array.ElementAt(i)))
                {
                    return true;
                }
            }

            return false;
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action.Invoke(item);
            }
        }
    }
}