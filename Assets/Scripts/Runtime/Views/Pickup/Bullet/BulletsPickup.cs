using System;
using System.Linq;
using Shooter.Model;
using Shooter.Model.Inventory;
using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    [RequireComponent(typeof(Collider))]
    public sealed class BulletsPickup : Pickup, IBulletsPickup
    {
        [SerializeField, Min(1)] private int _addBullets = 10;
        [SerializeField] private ItemData _weaponTypeForAddBullets;

        private IReadOnlyInventory<(IWeapon, IWeaponInput)> _inventory;

        public void Init(IReadOnlyInventory<(IWeapon, IWeaponInput)> inventory)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        }

        protected override void OnPicked()
        {
            foreach (var item in _inventory.Slots.Select(slot => slot.Item))
            {
                if (item.Data == _weaponTypeForAddBullets)
                {
                    var weapon = item.Model.Item1;
                    weapon.AddBullets(_addBullets);
                }
            }
        }
    }
}