using System;
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
        [SerializeField] private ItemGameObjectView _itemView;

        private IInventory<IWeapon> _inventory;
        private bool _enable = true;
        private IWeapon _weapon;

        [field: SerializeField] public WeaponData Data { get; private set; }

        public void Init(IInventory<IWeapon> inventory, IWeapon weapon)
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out CharacterMovement _))
            {
                if (_inventory.IsFull == false && _enable)
                {
                    var item = new Item<IWeapon>(_itemData, _weapon, _itemView);
                    _inventory.Add(item, 1);
                    gameObject.SetActive(false);
                    _enable = false;
                }
            }
        }
    }
}