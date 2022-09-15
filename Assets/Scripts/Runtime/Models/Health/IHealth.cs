namespace Shooter.Model
{
    public interface IHealth
    {
        public bool IsAlive { get; }
        
        public int StartValue { get; }
        
        public int Value { get; }

        public void TakeDamage(int damage);
        
        public void Heal(int amount);
        
        public bool CanHeal(int amount);
    }
}