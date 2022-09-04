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
        private IHealthTransformView _character;

        public void Init(IHealthTransformView character)
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

        private async void Attack(IHealthTransformView character)
        {
            await _attackAnimation.Play();

            if (NearCharacter())
            {
                if (character.Health.IsAlive)
                    character.Health.TakeDamage(_damage);
                Debug.Log("Attack");
            }
        }

        private bool NearCharacter()
        {
            var distance = (transform.position - _character.Position).sqrMagnitude;
            return distance <= _radius;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}