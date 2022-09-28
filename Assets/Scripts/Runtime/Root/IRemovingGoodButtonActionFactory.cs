using Shooter.Shop;

namespace Shooter.Model
{
    public interface IRemovingGoodButtonActionFactory
    {
        IButtonClickAction Create(IGood good);
    }
}