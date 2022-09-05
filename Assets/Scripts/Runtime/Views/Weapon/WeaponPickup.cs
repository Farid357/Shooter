using System;
using System.Linq;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class WeaponPickup : SerializedMonoBehaviour
    {
        [SerializeField] private ItemData _itemData;
        [SerializeField] private WeaponType _type;
        [SerializeField] private WeaponData _weaponData;
        [SerializeField] private IItemView _itemView;

        private IInventory<IWeapon> _inventory;
        private WeaponFactoryFromType _weaponFactory;

        public void Init(IInventory<IWeapon> inventory, IWeaponFactory weaponFactory)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _weaponFactory = new WeaponFactoryFromType(weaponFactory, _weaponData);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out CharacterMovement _))
            {
                if (_inventory.IsFull == false)
                {
                    var weapon = _weaponFactory.Create(_type);
                    var item = new Item<IWeapon>(_itemData, weapon, _itemView);
                    _inventory.Add(item, 1);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}