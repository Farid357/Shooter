namespace Shooter.Model
{
    public interface IHealthCollision
    {
        public void TryDamage(in int damage);
    }
}