using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Model
{
    public sealed class PotionView : MonoBehaviour, IPotionView
    {
        [SerializeField] private Image _screen;
        [SerializeField] private AudioSource _audio;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Color _usingColor = Color.red;
        
        public async Task VisualizeShot()
        {
            _audio.Play();
            _particleSystem.Play();
            var startColor = _screen.color;
            _screen.DOColor(_usingColor, 0.2f).OnComplete(() => _screen.DOColor(startColor, 0.2f));
            await Task.Delay(TimeSpan.FromSeconds(0.4f));
        }
    }
    
    public sealed class PotionGameObjectView : MonoBehaviour, IPotion
    {
        private IPotion _potion;

        public void Init(IPotion potion)
        {
            _potion = potion ?? throw new ArgumentNullException(nameof(potion));
        }
        
        public bool CanShoot => _potion.CanShoot;
        
        public void Shoot() => _potion.Shoot();

    }
}