using System.Linq;
using NUnit.Framework;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using UnityEngine;

namespace Shooter.Test
{
    public sealed class InventoryDropTest
    {
        [Test]
        public void InventoryDropItemCorrectly()
        {
            var inventory = new Inventory<IWeapon>(new DummyInventoryView());
            var slot = new InventorySlot<IWeapon>(new DummyItemSelector<IWeapon>(), new Item<IWeapon>(ScriptableObject.CreateInstance<ItemData>(), new DummyWeapon(), new DummyItemView()),3);
            inventory.Add(slot);
            inventory.Drop(slot);
            Assert.That(inventory.Slots.Count() == 1 && inventory.Slots.ElementAt(0).ItemsCount == 2);
            inventory.Drop(slot);
            inventory.Drop(slot);
            Assert.That(inventory.Slots.Count() == 0);
        }
    }
}