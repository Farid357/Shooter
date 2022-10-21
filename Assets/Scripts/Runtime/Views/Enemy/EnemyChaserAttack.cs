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

        private void Update()
        {
            if (_chaser.NearCharacter())
                Attack(_chaser.Character);
        }

        private async void Attack(IHealthTransformView character)
        {
            await _attackAnimation.Play();

            if (_chaser.NearCharacter() && character.Health.IsAlive)
            {
                character.Health.TakeDamage(_damage);
            }
        }
    }
}