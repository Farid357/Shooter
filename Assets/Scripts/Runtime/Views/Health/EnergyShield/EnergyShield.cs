using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnergyShield : MonoBehaviour, IEnergyShield
    {
        [SerializeField, Min(1)] private int _protection = 25;
        [SerializeField] private AudioSource _audio;
        [SerializeField] private HealthTransformView _character;
        private IHealth _characterHealthOnActivated;
        
        private bool IsActivated => _characterHealthOnActivated != null;
        
        public void Activate()
        {
            _characterHealthOnActivated = _character.Health;
            _character.Init(new Health(_characterHealthOnActivated.Value + _protection, new DummyHealthView()));
            _audio.Play();
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (IsActivated == false)
                return;
           
            if (_character.Health.Value <= _characterHealthOnActivated.Value - _protection)
            {
                _character.Init(_characterHealthOnActivated);
                _characterHealthOnActivated = null;
                gameObject.SetActive(false);
            }
        }
    }
}