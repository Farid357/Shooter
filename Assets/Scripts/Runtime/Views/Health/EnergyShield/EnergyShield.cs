using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnergyShield : MonoBehaviour, IHealthTransformView, IEnergyShield
    {
        [SerializeField, Min(1)] private int _protection = 25;
        [SerializeField] private AudioSource _audio;

        public IHealth Health { get; private set; }

        public Vector3 Position => transform.position;

        public void Activate()
        {
            Health = new Health(_protection, new DummyHealthView());
            _audio.Play();
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if(Health is null)
                return;
            
            if (Health.IsDied)
            {
                gameObject.SetActive(false);
            }
        }
    }
}