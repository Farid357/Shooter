using UnityEngine;

namespace Shooter.Tools
{
    public static class QuaternionUtils
    {
        public static Transform SetRotationZ(this Transform transform, float z)
        {
            var rotation = transform.rotation;
            transform.rotation = new Quaternion(rotation.x, rotation.y, z, rotation.w);
            return transform;
        }

        public static Transform SetRotationX(this Transform transform, float x)
        {
            var rotation = transform.rotation;
            transform.rotation = new Quaternion(x, rotation.y, rotation.z, rotation.w);
            return transform;
        }
    }
}