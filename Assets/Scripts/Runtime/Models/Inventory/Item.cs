using System;
using Shooter.GameLogic.Inventory;

namespace Shooter.Model.Inventory
{
    public readonly struct Item<T>
    {
        public readonly ItemData Data;
        public readonly T Object;

        public Item(ItemData data, T self)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Object = self ?? throw new ArgumentNullException(nameof(self));
        }
    }
}