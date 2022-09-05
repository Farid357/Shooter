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
        private ItemData _itemData;
        private DummyInventoryView _inventoryView = new();

        [SetUp]
        public void Setup()
        {
            _inventory = new Inventory<IWeapon>(_inventoryView);
            _itemData = ScriptableObject.CreateInstance<ItemData>();
        }
            
        [Test]
        public void InventoryAddsItemCorrect()
        {
            _inventory.Add(new Item<IWeapon>(_itemData, new DummyWeapon(), new DummyItemView()), 1);
            Assert.That(_inventory.Items.Count() == 1);
        }

        [Test]
        public void InventoryWillMakeFull()
        {
            for (var i = 0; i < 10; i++)
            {
                _inventory.Add(new Item<IWeapon>(_itemData, new DummyWeapon(), new DummyItemView()), 1);
            }
            
            Assert.That(_inventory.IsFull);
        }

        [Test]
        public void InventoryVisualizeNewItem()
        {
            _inventory.Add(new Item<IWeapon>(_itemData, new DummyWeapon(), new DummyItemView()), 1);
            Assert.That(_inventoryView.IsVisualized);
        }
    }
}