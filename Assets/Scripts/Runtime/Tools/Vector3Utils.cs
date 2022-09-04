using UnityEngine;

namespace Shooter.Tools
{
    public static class Vector3Utils
    {
        public static void SetXPosition(this Transform transform, float x)
        {
            var position = transform.position;
            transform.position = new Vector3(x, position.y, position.z);
        }
    }
}