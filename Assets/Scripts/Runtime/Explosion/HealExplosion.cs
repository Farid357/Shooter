using System.Linq;
using Shooter.Model;
using Shooter.Tools;
using UnityEngine;
using UnityEngine.Audio;

namespace Shooter.GameLogic
{
    public sealed class HealExplosion : Explosion
    {
        [SerializeField] private RayCastInSphereObjectsFinder _rayCastInSphereObjectsFinder;
        [SerializeField] private ParticleSystem _particlePrefab;
        [SerializeField] private AudioSource _explosionAudio;
        [SerializeField] private AudioMixerGroup _mixerGroup;

        public override void Thunder(int healAmount)
        {
            healAmount.TryThrowLessThanOrEqualsToZeroException();
            Instantiate(_particlePrefab, transform.position, Quaternion.identity).Play();
            var audio = Instantiate(_explosionAudio, transform);
            audio.outputAudioMixerGroup = _mixerGroup;
            audio.Play();

            var healthTransformViews = _rayCastInSphereObjectsFinder.Find<IHealthTransformView>();
            for (var i = 0; i < healthTransformViews.Count(); i++)
            {
                var health = healthTransformViews.ElementAt(i).Health;

                if (health != null && health.CanHeal(healAmount))
                    health.Heal(healAmount);
            }
        }
    }
}