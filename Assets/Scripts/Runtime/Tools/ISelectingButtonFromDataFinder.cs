using Shooter.Model;

namespace Shooter.Shop
{
    public interface ISelectingButtonFromDataFinder
    {
        IButton Find(IGoodData data);
    }
}