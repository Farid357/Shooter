using UnityEngine;

namespace Shooter.Tools
{
    public static class QuaternionUtils
    {
        public static Transform SetRotationZ(this Transform transform, float z)
        {
            var rotation = transform.rotation;
            transform.rotation = Quaternion.Euler(new Vector3(rotation.x, rotation.y, z));
            return transform;
        }

        public static Transform SetRotationX(this Transform transform, float x)
        {
            var rotation = transform.rotation;
            transform.rotation =  Quaternion.Euler(new Vector3(x, rotation.y, rotation.z));
            return transform;
        }
    }
}