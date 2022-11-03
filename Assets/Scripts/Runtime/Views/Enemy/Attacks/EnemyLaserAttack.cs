using System;
using Cysharp.Threading.Tasks;
using Shooter.Model;
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
        
        private IHealth Character => _chaser.Character.Health;

        private void Update()
        {
            if (!_chaser.NearCharacter())
                return;

            if((_chaser.Character.Position - transform.position).sqrMagnitude > _maxLaserDistance * _maxLaserDistance)
                return;

            UniTask.Create(async () =>
            {
                var characterBeforePosition = _chaser.Character.Position;
                _enemyAttackSound.Play();
                await _attackAnimation.Play();
                
                var magnitude = (_chaser.Character.Position - characterBeforePosition).sqrMagnitude;

                if (magnitude <= _distanceBetweenHitPositionAndAttack * _distanceBetweenHitPositionAndAttack)
                {
                    Debug.Log("ogo");
                    Attack();
                }
            });
        }

        private void LateUpdate()
        {
            _line.SetPosition(0, _laserStartPoint.position);
            _line.SetPosition(1, _chaser.Character.Position);
        }

        private void Attack()
        {
            if (Character.IsAlive)
                Character.TakeDamage(_damage);
        }
    }
}