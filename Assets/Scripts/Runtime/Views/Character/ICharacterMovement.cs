namespace Shooter.Model
{
    public interface ICharacterMovement : ICharacterTransform
    {
        public bool CanIncreaseSpeed { get; }

        public void IncreaseSpeedForSeconds(float increaseSpeed, float seconds);
    }
}