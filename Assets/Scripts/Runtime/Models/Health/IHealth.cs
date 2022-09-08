namespace Shooter.Model
{
    public interface IHealth
    {
        public bool IsAlive { get; }
        
        public int StartValue { get; }
        
        public int Value { get; }

        public void TakeDamage(in int damage);
        
        public void Heal(in int amount);
        
        public bool CanHeal(in int amount);
    }
}