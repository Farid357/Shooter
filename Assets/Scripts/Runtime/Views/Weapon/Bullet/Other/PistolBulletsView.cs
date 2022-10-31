using Shooter.Model;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class PistolBulletsView : SerializedMonoBehaviour, IBulletsView
    {
        [SerializeField] private TMP_Text _text;

        [field: SerializeField] public IShotView ShotView { get; private set; }

        public void Visualize(int bullets)
        {
            _text.gameObject.SetActive(true);
            _text.text = "\u221E";
        }

        public void Disable() => _text.gameObject.SetActive(false);
    }
}