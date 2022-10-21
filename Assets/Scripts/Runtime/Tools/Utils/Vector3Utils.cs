using UnityEngine;

namespace Shooter.Tools
{
    public static class Vector3Utils
    {
        public static Transform SetPositionX(this Transform transform, float x)
        {
            var position = transform.position;
            transform.position = new Vector3(x, position.y, position.z);
            return transform;
        }

        public static Vector3 Positive(this Vector3 vector)
        {
            return new Vector3(vector.x.Positive(), vector.y.Positive(), vector.z.Positive());
        }
    }
}