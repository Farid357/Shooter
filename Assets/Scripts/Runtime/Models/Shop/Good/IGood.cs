namespace Shooter.Shop
{
    public interface IGood
    {
        GoodData Data { get; }
        
        void Use();
    }
}