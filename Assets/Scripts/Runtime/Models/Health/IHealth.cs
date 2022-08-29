namespace Shooter.Model
{
    public interface IHealth
    {
        public bool IsAlive { get; }

        public void TakeDamage(in int damage);
    }
}