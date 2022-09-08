using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class ExplosiveBulletCollision : BulletCollision
    {
        [SerializeField] private Explosion _explosionPrefab;
        
        protected override void OnCollide(IHealth health, Vector3 point)
        {
            Explode(point);
        }

        private void Explode(Vector3 point)
        {
            Instantiate(_explosionPrefab, point, Quaternion.identity).Thunder();
        }
    }
}