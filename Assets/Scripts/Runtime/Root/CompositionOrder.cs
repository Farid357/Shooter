using System.Collections.Generic;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class CompositionOrder : MonoBehaviour
    {
        [SerializeField] private List<CompositeRoot> _roots;

        private void Awake() => _roots.ForEach(root => root.Compose());
    }
}