﻿using System.Threading.Tasks;
using Shooter.GameLogic.Inventory;
using Shooter.Model.Inventory;

public sealed class DummyItemView : IInventoryItemGameObjectView
{
    public async Task Show()
    {
        await Task.Yield();
    }

    public async Task Hide()
    {
        await Task.Yield();
    }
}