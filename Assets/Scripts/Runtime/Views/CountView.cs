using DG.Tweening;
using Shooter.Model;
using TMPro;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class CountView : MonoBehaviour, IView<int>
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private ScrambleMode _scrambleMode = ScrambleMode.Numerals;
        [SerializeField] private float _speedOfChangeText = 0.5f;

        public void Visualize(int count)
        {
            _text.DOText(count.ToString(), _speedOfChangeText, scrambleMode: _scrambleMode);
        }
    }
}