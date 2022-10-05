using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class ComputerPotionInput : IPotionInput
    {
        public bool HasInputed => Input.GetKeyDown(KeyCode.X);
    }
}