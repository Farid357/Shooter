using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnemyDeathView : MonoBehaviour, IDeathView
    {
        [SerializeField] private Enemy _enemy;

        public void VisualizeDeath() => _enemy.gameObject.SetActive(false);
    }
}