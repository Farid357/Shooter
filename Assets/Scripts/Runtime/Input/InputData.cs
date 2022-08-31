using UnityEngine;

namespace Shooter.GameLogic
{
    public readonly struct InputData
    {
        public readonly KeyCode KeyCode;
        public readonly Vector3 Direction;

        public InputData(KeyCode keyCode, Vector3 direction)
        {
            KeyCode = keyCode;
            Direction = direction;
        }
    }
}