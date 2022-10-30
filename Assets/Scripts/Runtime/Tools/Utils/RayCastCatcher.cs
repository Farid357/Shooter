using UnityEngine;

namespace Shooter.Tools
{
    public sealed class RayCastCatcher
    {
        public bool Hit<TComponent>(out TComponent component, Ray ray, out RaycastHit hit, float maxDistance) where TComponent : class
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

            component = null;
            hit = new();
            return false;
        }

        public bool Hit<TComponent>(out TComponent component, out RaycastHit hit, Ray ray, float maxDistance) where TComponent : class
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
        
        public bool Hit<TComponent>(out TComponent component, Ray ray, out RaycastHit hit) where TComponent : class
        {
            return Hit(out component, ray, out hit, float.PositiveInfinity);
        }
    }
}