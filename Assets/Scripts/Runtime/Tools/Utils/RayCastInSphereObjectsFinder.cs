using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Tools
{
    public sealed class RayCastInSphereObjectsFinder : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        
        [field: SerializeField, MinValue(0.01f)] public float Radius { get; private set; } = 3.5f;

        public IEnumerable<T> Find<T>()
        {
            var colliders = Physics.OverlapSphere(transform.position, Radius, _layerMask.value);

            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent(out T component))
                {
                    yield return component;
                }
            }
        }
    }
}