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
            var weapon = new DummyWeapon();
            var slot = new InventorySlot<IWeapon>(new DummyItemSelector<IWeapon>(), new Item<IWeapon>(ScriptableObject.CreateInstance<ItemData>(), weapon, new DummyItemView()),3);
            inventory.Add(slot);
            inventory.Drop(weapon);
            Assert.That(inventory.Slots.Count() == 1 && inventory.Slots.ElementAt(0).ItemsCount == 2);
            inventory.Drop(weapon);
            inventory.Drop(weapon);
            Assert.That(inventory.Slots.Count() == 0);
        }
    }
}