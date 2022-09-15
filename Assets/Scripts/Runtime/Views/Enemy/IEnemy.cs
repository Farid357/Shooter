namespace Shooter.Model
{
    public interface IEnemy
    {
        public IEnemyMovement Movement { get; }

        public void Enable();

        public IHealth Health { get; }

    }
}