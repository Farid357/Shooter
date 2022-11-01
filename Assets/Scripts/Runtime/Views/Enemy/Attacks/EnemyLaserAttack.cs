using Cysharp.Threading.Tasks;
using Shooter.Model;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnemyLaserAttack : SerializedMonoBehaviour, IEnemyAttack
    {
        [SerializeField] private IAttackAnimation _attackAnimation;
        [SerializeField] private EnemyToCharacterChaser _chaser;
        [SerializeField] private LineRenderer _line;
        [SerializeField, Min(2f)] private float _maxLaserDistance = 15f;
        [SerializeField, Min(0.01f)] private float _distanceBetweenHitPositionAndAttack;
        [SerializeField] private Transform _laserStartPoint;
        [SerializeField, ProgressBar(1, 100, 1, 0, 0)] private int _damage;
        [SerializeField] private EnemySound _enemyAttackSound;

        private readonly RayCastCatcher _raycastHit = new();

        private IHealth Character => _chaser.Character.Health;

        private void Update()
        {
            if (!_chaser.NearCharacter())
                return;

            var ray = new Ray(_laserStartPoint.position, _chaser.Character.Position);
            _line.SetPosition(0, _laserStartPoint.position);

            if (_raycastHit.Hit<ICharacterMovement>(out _, out var hit, ray, _maxLaserDistance))
            {
                UniTask.Create(async () =>
                {
                    var point = hit.point;
                    _enemyAttackSound.Play();
                    await _attackAnimation.Play();
                    var magnitude = (hit.point - point).sqrMagnitude;

                    if (magnitude <= _distanceBetweenHitPositionAndAttack * _distanceBetweenHitPositionAndAttack)
                    {
                        _line.SetPosition(1, hit.point);
                        Attack();
                    }
                });
            }
            
            else
            {
                _line.SetPosition(1, ray.GetPoint(_maxLaserDistance));
            }
        }

        private void Attack()
        {
            if (Character.IsAlive)
                Character.TakeDamage(_damage);
        }
    }
}