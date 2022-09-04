using System;
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class HealthTransformView : MonoBehaviour, IHealthTransformView
    {
        public IHealth Health { get; private set; }
        
        public Vector3 Position => transform.position;

        public void Init(IHealth health)
        {
            Health = health ?? throw new ArgumentNullException(nameof(health));
        }
    }
}