using UnityEngine;

namespace Shooter.Model
{
    public interface ICharacterTransform
    {
        public Vector3 Position { get; }

        public Quaternion Rotation { get; }

        public void Rotate(Vector3 rotation);
    }
}