using UnityEngine;

namespace Shooter.Tools
{
    public static class RayCastUtils
    {
        public static bool Hit<TComponent>(this RaycastHit raycast, out TComponent component, Ray ray, float maxDistance) where TComponent : class
        {
            if (Physics.Raycast(ray, out var hit, maxDistance))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent(out TComponent t))
                    {
                        component = t;
                        return true;
                    }
                }
            }

            component = null;
            return false;
        }

        public static bool Hit<TComponent>(this RaycastHit raycast, out TComponent component, out RaycastHit hit, Ray ray, float maxDistance) where TComponent : class
        {
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent(out TComponent t))
                    {
                        component = t;
                        return true;
                    }
                }
            }

            hit = new();
            component = null;
            return false;
        }
        
        public static bool Hit<TComponent>(this RaycastHit raycast, out TComponent component, Ray ray) where TComponent : class
        {
            return Hit(raycast, out component, ray, float.PositiveInfinity);
        }
    }
}