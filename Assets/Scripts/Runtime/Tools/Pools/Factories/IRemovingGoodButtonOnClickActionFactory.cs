using Shooter.Model;
using Shooter.Shop;

namespace Shooter.Tools
{
    public interface IRemovingGoodButtonOnClickActionFactory
    {
        public IButtonClickAction Create(IGood good);
    }
}