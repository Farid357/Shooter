namespace Shooter.Tools
{
    public static class FloatUtils
    {
        public static float TryThrowLessThanOrEqualsToZeroException(this float number)
        {
            if (number <= 0)
                throw new LessThanOrEqualsToZeroException(nameof(number));

            return number;
        }

        public static float Positive(this float number)
        {
            if (number < 0)
                number = -number;

            return number;
        }
    }
}
