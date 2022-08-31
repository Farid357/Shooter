using System.Collections.Generic;

namespace Shooter.Tools
{
    public interface IGameObjectsContainerFactory<T> : IFactory<T>
    {
        public IEnumerable<T> CreatedObjects { get; }
    }
}