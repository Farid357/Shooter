using DG.Tweening;
using Shooter.Model;
using TMPro;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnergyWeaponBulletsCountView : MonoBehaviour, IView<int>
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private ScrambleMode _scrambleMode = ScrambleMode.None;
        [SerializeField, Min(0f)] private float _changeTextSpeed = 1.5f;
        [SerializeField, Min(1)] private int _increaseCoefficient = 10;
        
        public void Visualize(int bullets)
        {
            _text.DOText((bullets * _increaseCoefficient).ToString(), _changeTextSpeed, scrambleMode: _scrambleMode);
        }
    }
}