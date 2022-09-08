using System;
using System.Collections.Generic;

namespace Shooter.Model
{
    public sealed class DigitsFormatter
    {
        private readonly List<(int, string)> _digitsPrefixes = new()
        {
            (100, "h"),
            (1000, "k"),
            (1000000, "m"),
            (1000000000, "b")
        };

        public string TryFormat(int count)
        {
            var text = count.ToString();

            foreach (var (digit, prefix) in _digitsPrefixes)
            {
                if (count < digit)
                    continue;

                var value = Math.Round(count / (double)digit, 1);
                text = $"{value}{prefix}";
            }

            return text;
        }
    }
}