using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnemyHealthView : SerializedMonoBehaviour, IHealthView
    {
        [SerializeField] private IDeathView _deathView;

        public void Visualize(int health)
        {
            if (health == 0)
                _deathView.VisualizeDeath();
        }
    }
}