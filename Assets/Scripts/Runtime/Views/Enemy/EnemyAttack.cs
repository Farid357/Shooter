using System;
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnemyAttack : MonoBehaviour
    {
        [SerializeField, Range(1, 100)] private int _damage = 15;
        [SerializeField] private AttackAnimation _attackAnimation;
        [SerializeField, Min(0.1f)] private float _radius = 0.5f;
        private IHealthCollision _character;

        public void Init(IHealthCollision character)
        {
            _character = character ?? throw new ArgumentNullException(nameof(character));
        }

        private void Update()
        {
            if (NearCharacter())
            {
                Debug.Log("near");
                Attack(_character);
            }
        }

        private async void Attack(IHealthCollision character)
        {
            await _attackAnimation.Play();

            if (NearCharacter())
            {
                character.TryTakeDamage(_damage);
                Debug.Log("Attack");
            }
        }

        private bool NearCharacter()
        {
            var distance = (_character.Position - transform.position).sqrMagnitude;
            return distance <= _radius * _radius;
        }
    }
}