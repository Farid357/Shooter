using UnityEngine;

namespace Shooter.Model
{
    public interface IHealthTransformView
    {
        public IHealth Health { get; }
        
        public Vector3 Position { get; }
    }
}