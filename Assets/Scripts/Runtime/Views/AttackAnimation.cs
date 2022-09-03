using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Shooter.GameLogic
{
    [RequireComponent(typeof(Animator))]
    public sealed class AttackAnimation : MonoBehaviour
    {
        [SerializeField, Min(0.1f)] private float _triggerSeconds = 0.7f;
        [SerializeField] private string _name = "Enemy Attack";
        private Animator _animator;
        private int _id;

        private void OnDisable()
        {
            _animator = GetComponent<Animator>();
            _id = Animator.StringToHash(_name);
        }

        public async UniTask Play()
        {
            _animator.SetTrigger(_id);
            await UniTask.Delay(TimeSpan.FromSeconds(_triggerSeconds / 2f));
        }
    }
}