using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnemyDeathView : MonoBehaviour, IDeathView
    {
        public void VisualizeDeath() => gameObject.SetActive(false);
        
    }
}