using System;
using Shooter.Model;
using TMPro;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class SecondsView : MonoBehaviour, IView<float>
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Color _littleTimeColor;
        [SerializeField, Min(0.1f)] private float _littleTime = 5f;
        private Color _startColor;

        private void OnEnable() => _startColor = _text.color;

        public void Visualize(float seconds)
        {
            _text.color = seconds <= _littleTime ? _littleTimeColor : _startColor;
            
            if(seconds == 0f)
                _text.color = Color.clear;
            
            _text.text = Math.Round(seconds, 1).ToString();
        }
    }
}