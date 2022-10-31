using Shooter.Model;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class BulletsView : SerializedMonoBehaviour, IBulletsView
    {
        [SerializeField] private IView<int> _view;
        [SerializeField] private Color _zeroBullets = Color.red;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_Text _bulletsEnded;
        private Color _startColor;
        
        [field: SerializeField] public IShotView ShotView { get; private set; }
        
        private void OnEnable() => _startColor = _text.color;

        public void Visualize(int bullets)
        {
            _text.gameObject.SetActive(true);
            _text.color = bullets == 0 ? _zeroBullets : _startColor;
            _view.Visualize(bullets);
            _bulletsEnded.gameObject.SetActive(bullets == 0);
        }

        public void Disable() => _text.gameObject.SetActive(false);
    }
}