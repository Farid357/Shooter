using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnemyDeathView : MonoBehaviour, IDeathView
    {
        [SerializeField] private ParticleSystem _particlePrefab;
        
        public void VisualizeDeath()
        {
            Instantiate(_particlePrefab, transform.position, Quaternion.identity).Play();
            gameObject.SetActive(false);
        }
    }
}