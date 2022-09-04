using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Shooter.Model;
using UnityEngine;
using UnityEngine.AI;

namespace Shooter.GameLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class StandartEnemyMovement : MonoBehaviour, IEnemyMovement
    {
        [SerializeField, Min(1f)] private float _rotateSpeed = 2.5f;

        private NavMeshAgent _navMesh;
        private ICharacter _character;
        private bool _needMove;
        private bool _needRotate;

        private void OnEnable() => _navMesh = GetComponent<NavMeshAgent>();

        private void Update()
        {
            if (_needMove)
                Move();

            if (_needRotate)
            {
                Rotate();
            }
        }

        public void Init(ICharacter character)
        {
            _character = character ?? throw new ArgumentNullException(nameof(character));
        }

        public void MoveToCharacter() => _needMove = true;

        public void RotateToCharacter() => _needRotate = true;

        private void Move()
        {
            if (_needMove == false || _character is null)
                throw new InvalidOperationException(nameof(Move));

            _navMesh.SetDestination(_character.Position);
        }

        private async UniTask Rotate()
        {
            while (transform.rotation != _character.Rotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _character.Rotation, _rotateSpeed * Time.deltaTime);
                await Task.Yield();
            }

            _needRotate = false;
        }
    }
}