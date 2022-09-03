using UnityEngine;

namespace Shooter.Model
{
    public interface ICharacter
    {
        public Vector3 Position { get; }
        
        public Quaternion Rotation { get; }
    }
}