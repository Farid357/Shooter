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
    }
}