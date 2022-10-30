using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class ExplosiveBarrelExplosionView : SerializedMonoBehaviour, IHealthView
    {
        [SerializeField] private Explosion _explosion;
        [SerializeField, Range(1, 100)] private int _damage = 2;
        
        public void Visualize(int health)
        {
            if (health == 0)
            {
                _explosion.Thunder(_damage);
                gameObject.SetActive(false);
            }
        }
    }
}