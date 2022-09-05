using UnityEditor.AI;
using UnityEngine;

namespace Shooter.Tools
{
    public sealed class NavMeshBaker : MonoBehaviour
    {
        public void Bake()
        {
#if UNITY_EDITOR

           // NavMeshBuilder.BuildNavMesh();
#endif
        }
    }
}