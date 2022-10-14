using System;
using Sirenix.OdinInspector;

namespace Shooter.Root
{
    public abstract class CompositeRoot : SerializedMonoBehaviour
    {
        public abstract void Compose();
    }
}