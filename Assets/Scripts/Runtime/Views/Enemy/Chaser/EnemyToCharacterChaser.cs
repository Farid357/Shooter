using System;
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnemyToCharacterChaser : MonoBehaviour
    {
        [SerializeField, Min(0.1f)] private float _radius = 0.5f;
        [SerializeField] private Transform _center;
        [SerializeField] private bool _showGizmas;
        
        public IHealthTransformView Character { get; private set; }

        public void Init(IHealthTransformView character)
        {
            Character = character ?? throw new ArgumentNullException(nameof(character));
        }

        public bool NearCharacter()
        {
            var distance = (_center.position - Character.Position).sqrMagnitude;
            return distance <= _radius * _radius;
        }

        private void OnDrawGizmosSelected()
        {
            if (_showGizmas)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(_center.position, _radius);
            }
        }
    }
}