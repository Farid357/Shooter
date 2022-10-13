using System;
using System.Collections.Generic;
using Shooter.Model;
using Shooter.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.GameLogic
{
    public sealed class AudioSlider : MonoBehaviour, ISlider
    {
        [SerializeField] private Slider _slider;
        private readonly List<ISliderChangedValueAction> _actions = new();

        public void Subscribe(ISliderChangedValueAction sliderChangedValueAction)
        {
            if (sliderChangedValueAction is null) 
                throw new ArgumentNullException(nameof(sliderChangedValueAction));
            
            _actions.Add(sliderChangedValueAction);
            _slider.onValueChanged.AddListener(sliderChangedValueAction.Change);
        }

        public void SetFloat(float value) => _slider.value = value.TryThrowLessThanOrEqualsToZeroException();
        
        public void Enable() => _slider.gameObject.SetActive(true);
        
        public void Disable() => _slider.gameObject.SetActive(false);

        private void OnDestroy()
        {
            _actions.ForEach(action => _slider.onValueChanged.RemoveListener(action.Change));
        }
    }
}