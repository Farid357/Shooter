using System;

namespace Shooter.Tools
{
    public static class RandomUtils
    {
        public static T GetRandomFromArray<T>(this T[] array)
        {
            var randomIndex = new Random().Next(0, array.Length);
            return array[randomIndex];
        }
    }
}