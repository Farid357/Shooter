using System.Collections.Generic;
using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnemyHealthViewWithAnimations : SerializedMonoBehaviour, IHealthView
    {
        [SerializeField] private List<EnemyAnimationData> _animationData;
        [SerializeField] private Animator _animator;
        [SerializeField] private IHealthView _healthView;

        public void Visualize(int health)
        {
            for (var i = 0; i < _animationData.Count; i++)
            {
                if (health <= _animationData[i].NeedHealth)
                {
                    _animator.Play(_animationData[i].Name);
                }
            }

            _healthView.Visualize(health);
        }
    }
}