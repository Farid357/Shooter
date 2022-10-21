using System;
using Cysharp.Threading.Tasks;
using Shooter.Tools;
using UnityEngine;
using UnityEngine.AI;

namespace Shooter.GameLogic
{
    public sealed class EnemyNavMeshAgent : IEnemyNavMeshAgent
    {
        private readonly NavMeshAgent _navMeshAgent;

        public EnemyNavMeshAgent(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent ?? throw new ArgumentNullException(nameof(navMeshAgent));
        }

        public bool CanIncreaseSpeed { get; private set; }

        public async UniTaskVoid IncreaseSpeedForSeconds(float increaseSpeed, float seconds)
        {
            if (_navMeshAgent.speed >= increaseSpeed)
                throw new InvalidOperationException("This speed less or equals to current speed!");

            if (CanIncreaseSpeed == false)
                throw new InvalidOperationException("Can't increase speed!");;

            CanIncreaseSpeed = false;
            await SetSpeedForSeconds(increaseSpeed, seconds);
            CanIncreaseSpeed = true;
        }

        public async UniTaskVoid SlowDownForSeconds(float newSpeed, float seconds)
        {
            if (_navMeshAgent.speed <= newSpeed)
                throw new InvalidOperationException("This speed greater or equals to current speed!");

            await SetSpeedForSeconds(newSpeed, seconds);
        }

        private async UniTask SetSpeedForSeconds(float newSpeed, float seconds)
        {
            newSpeed.TryThrowLessThanOrEqualsToZeroException();
            seconds.TryThrowLessThanOrEqualsToZeroException();
            var speed = _navMeshAgent.speed;
            _navMeshAgent.speed = newSpeed;
            await UniTask.Delay(TimeSpan.FromSeconds(seconds));
            _navMeshAgent.speed = speed;
        }
    }
}