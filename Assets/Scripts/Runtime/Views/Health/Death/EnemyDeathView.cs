using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnemyDeathView : MonoBehaviour, IDeathView
    {
        [SerializeField] private ParticleSystem _particlePrefab;

        public void VisualizeDeath()
        {
            Instantiate(_particlePrefab, transform.position, _particlePrefab.transform.rotation).Play();
            gameObject.SetActive(false);
        }

        [Button("Play Effect", ButtonSizes.Small, ButtonStyle.CompactBox), GUIColor(1, 0, 1)]
        public void PlayEffect()
        {
            Instantiate(_particlePrefab, transform.position, _particlePrefab.transform.rotation, transform).Play();
        }
    }
}
