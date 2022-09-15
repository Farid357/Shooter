using Unity.AI.Navigation;
using UnityEngine;

namespace Shooter.Tools
{
    public sealed class NavMeshBaker : MonoBehaviour, INavMeshBaker
    {
        [SerializeField] private NavMeshSurface[] _surfaces;

        public void Bake()
        {
            // foreach (var surface in _surfaces)
            // {
            //     surface.BuildNavMesh();
            // }
        }
    }
}