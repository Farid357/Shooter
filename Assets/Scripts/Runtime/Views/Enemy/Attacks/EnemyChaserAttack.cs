using System;
using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnemyChaserAttack : SerializedMonoBehaviour, IEnemyAttack
    {
        [SerializeField] private IAttackAnimation _attackAnimation;
        [SerializeField] private EnemyToCharacterChaser _chaser;
        [SerializeField, Range(1, 100)] private int _damage = 15;
        [SerializeField] private EnemySound _enemyAttackSound;
        private bool _isNotAttacking = true;

        private void Update()
        {
            if (_chaser.NearCharacter() && _isNotAttacking)
                Attack(_chaser.Character);
        }

        private async void Attack(IHealthTransformView character)
        {
            _isNotAttacking = false;
            _enemyAttackSound.Play();
            await _attackAnimation.Play();
            
            if (_chaser.NearCharacter() && character.Health.IsAlive)
            {
                character.Health.TakeDamage(_damage);
            }

            _isNotAttacking = true;
        }
    }
}