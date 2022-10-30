using System;
using Cysharp.Threading.Tasks;
using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnemyDeathViewWithAnimation : SerializedMonoBehaviour, IDeathView
    {
        [SerializeField] private IDeathView _deathView;
        [SerializeField] private Animator _animator;
        [SerializeField] private string _dieAnimationName;
        [SerializeField, Min(0.01f)] private float _beforeDieSeconds = 1.5f;

        public void VisualizeDeath()
        {
            UniTask.Create(async () =>
            {
                _animator.Play(_dieAnimationName);
                await UniTask.Delay(TimeSpan.FromSeconds(_beforeDieSeconds));
                _deathView.VisualizeDeath();
            });
        }
    }
}