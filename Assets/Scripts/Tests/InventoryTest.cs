using System.Linq;
using NUnit.Framework;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using UnityEngine;

namespace Shooter.Test
{
    [TestFixture]
    public sealed class InventoryTest
    {
        private Inventory<IWeapon> _inventory;
        private DummyInventoryView _inventoryView = new();
        private InventorySlot<IWeapon> _slot;
        
        [SetUp]
        public void Setup()
        {
            _inventory = new Inventory<IWeapon>(_inventoryView);
            var itemData = ScriptableObject.CreateInstance<ItemData>();
            var item = new Item<IWeapon>(itemData, new DummyWeapon(), new DummyItemView());
            _slot = new InventorySlot<IWeapon>(new DummyItemSelector<IWeapon>(), item);
        }
            
        [Test]
        public void InventoryAddsItemCorrect()
        {
            _inventory.Add(_slot, 1);
            Assert.That(_inventory.Slots.Count() == 1);
        }

        [Test]
        public void InventoryWillMakeFull()
        {
            for (var i = 0; i < 10; i++)
            {
                _inventory.Add(_slot, 1);
            }
            
            Assert.That(_inventory.IsFull);
        }

        [Test]
        public void InventoryVisualizeNewItem()
        {
            _inventory.Add(_slot, 1);
            Assert.That(_inventoryView.IsVisualized);
        }
    }
}