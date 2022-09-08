using DG.Tweening;
using Shooter.Model;
using TMPro;
using UnityEngine;

namespace Shooter.GameLogic
{
    public  sealed  class  LineVisualization : MonoBehaviour, IView<string>
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _seconds = 1.5f;
        [SerializeField] private ScrambleMode _scrambleMode = ScrambleMode.Numerals;
        
        public void Visualize(string line)
        {
            _text.DOText(line, _seconds, scrambleMode: _scrambleMode);
        }
    }
}