using Shooter.Model;

namespace Shooter.Tools
{
    public static class ThrowingWeaponUtils
    {
        public static bool IsNotThrowingWeapon<T>(this T type)
        {
            return type is not IThrowingWeapon;
        }
    }
}