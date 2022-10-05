namespace Shooter.Shop
{
    public interface IGood
    {
        IGoodData Data { get; }
        
        void Use();
    }
}