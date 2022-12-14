using System;
using Shooter.Model;
using UnityEngine;
using UnityEngine.AI;

namespace Shooter.GameLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class StandartEnemyMovement : MonoBehaviour, IEnemyMovement
    {
        private ICharacterTransform _character;
        private NavMeshAgent _navMesh;
        private bool _needMove;

        public IEnemyNavMeshAgent Agent { get; private set; }

        public void Init(ICharacterTransform character)
        {
            _character ??= character ?? throw new ArgumentNullException(nameof(character));
            Agent ??= new EnemyNavMeshAgent(_navMesh);
        }
        
        private void OnEnable() => _navMesh ??= GetComponent<NavMeshAgent>();

        private void Update()
        {
            if (_needMove)
                Move();
        }
        
        public void MoveToCharacter() => _needMove = true;

        private void Move() => _navMesh.SetDestination(_character.Position);
        
    }
}