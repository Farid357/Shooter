using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class RandomAttackAnimationFromList : SerializedMonoBehaviour, IAttackAnimation
    {
        [SerializeField] private List<IAttackAnimation> _attackAnimations;
        
        public async UniTask Play()
        {
            var randomIndex = Random.Range(0, _attackAnimations.Count);
            await _attackAnimations[randomIndex].Play();
        }
    }
}