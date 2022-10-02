namespace Shooter.Model
{
    public interface IHealth
    {
        bool IsDied { get; }
        
        bool IsAlive { get; }
       
        int StartValue { get; }
       
        int Value { get; }

        void TakeDamage(int damage);
       
        void Heal(int amount);
       
        bool CanHeal(int amount);
    }
}