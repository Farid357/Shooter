using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    [RequireComponent(typeof(BulletCollision))]
    public sealed class ReboundBulletCollision : BulletCollision
    {
        [SerializeField] private BulletMovement _movement;
        [SerializeField, Range(1, 30)] private int _maxReboundsCount;
        
        private BulletCollision _bulletCollision;
        private int _reboundsCount;

        public override bool CanIncreaseDamage => _bulletCollision.CanIncreaseDamage;
        
        private void OnEnable()
        {
            _bulletCollision ??= GetComponent<BulletCollision>();
            _reboundsCount = 0;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_reboundsCount < _maxReboundsCount && collision.gameObject.GetComponent<IHealthTransformView>() == null)
            {
                _reboundsCount++;
                var direction = Vector3.Reflect(transform.position, collision.contacts[0].normal);
                _movement.Throw(direction);
            }
            
            else
            {
                gameObject.SetActive(false);
            }
        }

        public override void IncreaseDamageForSeconds(int damage, float seconds) => _bulletCollision.IncreaseDamageForSeconds(damage, seconds);
        
    }
}