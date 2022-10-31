using DG.Tweening;
using Shooter.Model;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnergyWeaponBulletsCountView : SerializedMonoBehaviour, IBulletsView
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private ScrambleMode _scrambleMode = ScrambleMode.None;
        [SerializeField, Min(0f)] private float _changeTextSpeed = 1.5f;
        [SerializeField, Min(1)] private int _increaseCoefficient = 10;

        [field: SerializeField] public IShotView ShotView { get; private set; }

        public void Visualize(int bullets)
        {
            _text.gameObject.SetActive(true);
            _text.DOText((bullets * _increaseCoefficient).ToString(), _changeTextSpeed, scrambleMode: _scrambleMode);
        }

        public void Disable() => _text.gameObject.SetActive(false);
    }
}