using UnityEditor.AI;

namespace Shooter.Tools
{
    public sealed class NavMeshBaker
    {
        public void Bake()
        {
#if UNITY_EDITOR

            NavMeshBuilder.BuildNavMesh();
#endif
        }
    }
}