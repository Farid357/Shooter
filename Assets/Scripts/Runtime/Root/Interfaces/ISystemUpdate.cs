using Shooter.Model;

namespace Shooter.Root
{
    public interface ISystemUpdate
    {
        public void Add(params IUpdateble[] updatebles);

        public void Remove(IUpdateble updateble);
        
    }
}