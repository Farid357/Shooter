using System;
using Shooter.GameLogic.Inventory;

namespace Shooter.Model.Inventory
{
    public readonly struct Item<T>
    {
        public readonly ItemData Data;
        public readonly IGameObjectItemView View;
        public readonly T Model;

        public Item(ItemData data, T self, IGameObjectItemView view)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            View = view ?? throw new ArgumentNullException(nameof(view));
            Model = self ?? throw new ArgumentNullException(nameof(self));
        }
    }
}