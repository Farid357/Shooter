using System.Collections.Generic;

namespace Shooter.Model
{
    public sealed class DigitsFormatter
    {
        private readonly List<(int, string)> _digitsPrefixes = new()
        {
            (100, "H"),
            (1000, "K"),
            (1000000, "M"),
            (1000000000, "B")
        };

        public string TryFormat(int count)
        {
            for (var i = 0; i < _digitsPrefixes.Count - 1; ++i)
            {
                var (currentValue, currentPostfix) = _digitsPrefixes[i];
                var (nextValue, nextPostfix) = _digitsPrefixes[i + 1];

                var nextIsLast = i >= _digitsPrefixes.Count - 2;

                if (nextValue > count)
                {
                    float result = (float)count / currentValue;
                    return result.ToString(result >= 100 ? "0" : "0.0").Replace(".0", "") + currentPostfix;
                }

                if (nextIsLast)
                {
                    float result = (float)count / nextValue;
                    return result.ToString(result >= 100 ? "0" : "0.0").Replace(".0", "") + nextPostfix;
                }
            }

            return string.Empty;
        }
    }
}