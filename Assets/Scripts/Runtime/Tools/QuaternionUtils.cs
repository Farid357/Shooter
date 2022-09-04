using UnityEngine;

namespace Shooter.Tools
{
    public static class QuaternionUtils
    {
        public static void SetZRotation(this Transform transform, float z)
        {
            var rotation = transform.rotation;
            transform.rotation = new Quaternion(rotation.x, rotation.y, z, rotation.w);
        }
    }
}