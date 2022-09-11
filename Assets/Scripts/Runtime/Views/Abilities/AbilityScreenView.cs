using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Shooter.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.GameLogic
{
    public sealed class AbilityScreenView : MonoBehaviour, IAbilityView
    {
        [SerializeField] private Color _applyImageColor = Color.blue;
        [SerializeField] private Image _image;
        [SerializeField, Min(0.1f)] private float _changeColorSpeed = 1.5f;

        public void VisualizeApply(float applySeconds) => StartVisualizeApply(applySeconds);

        private async UniTaskVoid StartVisualizeApply(float applySeconds)
        {
            _image.DOColor(_applyImageColor, _changeColorSpeed);
            await UniTask.Delay(TimeSpan.FromSeconds(applySeconds));
            _image.color = Color.clear;
        }
    }
}