using Unity.AI.Navigation;
using UnityEngine;

namespace Shooter.Tools
{
    public sealed class NavMeshBaker : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface[] _surfaces;

        public void Bake()
        {
            foreach (var surface in _surfaces)
            {
                surface.BuildNavMesh();
            }
        }
    }
}
