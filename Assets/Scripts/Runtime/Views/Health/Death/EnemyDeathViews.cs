using Shooter.Model;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnemyDeathViews : SerializedMonoBehaviour, IDeathView
    {
        [SerializeField] private IDeathView[] _deathViews;
        
        public void VisualizeDeath()
        {
            _deathViews.ForEach(view => view.VisualizeDeath());
        }
    }
}