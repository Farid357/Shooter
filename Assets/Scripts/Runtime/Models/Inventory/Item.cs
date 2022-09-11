﻿using System;
using Shooter.GameLogic.Inventory;

namespace Shooter.Model.Inventory
{
    public readonly struct Item<T>
    {
        public readonly ItemData Data;
        public readonly IItemView View;
        public readonly T Object;

        public Item(ItemData data, T self, IItemView view)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            View = view ?? throw new ArgumentNullException(nameof(view));
            Object = self ?? throw new ArgumentNullException(nameof(self));
        }

        public bool ObjectIsWeapon(out IWeapon weapon)
        {
            weapon = Object as IWeapon;
            return weapon is not null;
        }
    }
}