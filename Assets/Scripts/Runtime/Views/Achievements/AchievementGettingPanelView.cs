using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Model
{
    public sealed class AchievementGettingPanelView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;
        [SerializeField, Min(0.1f)] private float _showSpeed = 1.5f;
        [SerializeField, Min(0.1f)] private float _hideSpeed = 0.4f;

        public void Show(string name)
        {
            _text.text = name;
            _image.DOFillAmount(1, _showSpeed).OnComplete(() => Hide().Forget());
        }

        private async UniTaskVoid Hide()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_hideSpeed));
            _image.DOFillAmount(0, _hideSpeed);
        }
    }
}