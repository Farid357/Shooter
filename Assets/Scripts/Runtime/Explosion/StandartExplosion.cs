using System.Linq;
using Shooter.Model;
using Shooter.Tools;
using UnityEngine;
using UnityEngine.Audio;

namespace Shooter.GameLogic
{
    public sealed class StandartExplosion : Explosion
    {
        [SerializeField] private RayCastInSphereObjectsFinder _rayCastInSphereObjectsFinder;
        [SerializeField] private ParticleSystem _explosionParticlePrefab;
        [SerializeField] private AudioSource _explosionAudio;
        [SerializeField] private AudioMixerGroup _mixerGroup;

        public override void Thunder(int damage)
        {
            Instantiate(_explosionParticlePrefab, transform.position, Quaternion.identity).Play();
            var audio = Instantiate(_explosionAudio, transform);
            audio.outputAudioMixerGroup = _mixerGroup;
            audio.Play();

            var healthTransformViews = _rayCastInSphereObjectsFinder.Find<IHealthTransformView>();
            if(healthTransformViews.Count() == 0)
                return;
            
            foreach (var healthTransformView in healthTransformViews)
            {
                TryDamage(healthTransformView.Health, damage);
            }
        }

        private void TryDamage(IHealth health, int damage)
        {
            damage.TryThrowLessThanOrEqualsToZeroException();

            if (health.IsAlive)
                health.TakeDamage(damage);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, _rayCastInSphereObjectsFinder.Radius);
        }
    }
}