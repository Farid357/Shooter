using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class Barrel : SerializedMonoBehaviour
    {
        [SerializeField] private IHealthView _explosionView;
        [SerializeField] private HealthTransformView _healthTransformView;
        [SerializeField, ProgressBar(5, 100, r: 1, g: 0, b: 0)] private int _health = 10;
        
        private void OnEnable()
        {
            IHealth health = new Health(_health, _explosionView);
            _healthTransformView.Init(health);
        }
    }
}