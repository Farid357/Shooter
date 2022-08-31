using Shooter.Model;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class BulletsView : SerializedMonoBehaviour, IView<int>
    {
        [SerializeField] private IView<int> _view;
        [SerializeField] private Color _zeroBullets = Color.red;
        [SerializeField] private TMP_Text _text;
        private Color _startColor;

        private void OnEnable() => _startColor = _text.color;

        public void Visualize(int bullets)
        {
            _text.color = bullets == 0 ? _zeroBullets : _startColor;
            _view.Visualize(bullets);
        }
    }
}