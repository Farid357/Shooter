using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnemyHealthView : MonoBehaviour, IHealthView
    {
        [SerializeField] private EnemyDeathView _deathView;
        
        public void Visualize(int health)
        {
            if(health == 0)
                _deathView.VisualizeDeath();
        }
    }
}