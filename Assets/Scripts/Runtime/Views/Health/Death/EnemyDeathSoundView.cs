using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnemyDeathSoundView : MonoBehaviour, IDeathView
    {
        [SerializeField] private EnemySound _enemySound;
        [SerializeField] private Enemy _enemy;
        
        public void VisualizeDeath()
        {
            _enemy.gameObject.SetActive(false);
            _enemySound.Play();
        }
    }
}