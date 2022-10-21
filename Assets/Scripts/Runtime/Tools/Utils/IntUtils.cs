namespace Shooter.Tools
{
    public static class IntUtils
    {
        public static int TryThrowLessThanOrEqualsToZeroException(this int number)
        {
            if (number <= 0)
                throw new LessThanOrEqualsToZeroException(nameof(number));

            return number;
        }
    }
}
