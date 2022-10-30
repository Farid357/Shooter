namespace Shooter.Shop
{
    public interface IGoodView
    {
        void Visualize(IGoodData good);

        void Destroy();
    }
}