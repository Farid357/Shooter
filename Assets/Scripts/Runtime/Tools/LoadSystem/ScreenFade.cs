using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Shooter.LoadSystem
{
    public sealed class ScreenFade : MonoBehaviour, IScreenFade
    {
        [SerializeField] private Image _screen;
        [SerializeField] private float _fadeInSeconds = 1.5f;
        [SerializeField] private float _fadeOutSeconds = 2f;
        
        public event Action OnDarkened;
        
        private void Start() => DontDestroyOnLoad(gameObject);

        public void FadeIn()
        {
            _screen.DOFade(1, _fadeInSeconds).SetAutoKill(false).OnComplete(new TweenCallback(() => OnDarkened?.Invoke()));
        }

        public void FadeOut() => _screen.DOFade(0, _fadeOutSeconds);
    }
}
