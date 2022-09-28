namespace Shooter.GameLogic
{
    public interface IBulletCollision
    {
        public void IncreaseDamageForSeconds(int damage, float seconds);

        public bool CanIncreaseDamage { get; }

        public int Damage { get; }
    }
}