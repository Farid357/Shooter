using System;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Root;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public abstract class WeaponPickup : SerializedMonoBehaviour
    {
        [SerializeField] private ItemData _itemData;
        [SerializeField] private ItemGameObjectView _itemView;

        private IInventory<(IWeapon, IWeaponInput)> _inventory;
        private (IWeapon Model, IWeaponInput Input) _weapon;
        private IPlayerRoot _playerRoot;
        private bool _enable = true;

        public void Init(IInventory<(IWeapon, IWeaponInput)> inventory, IPlayerRoot playerRoot, (IWeapon Model, IWeaponInput Input) weapon)
        {
            _weapon = weapon;
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _playerRoot = playerRoot ?? throw new ArgumentNullException(nameof(playerRoot));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out CharacterMovement _))
            {
                if (_inventory.IsFull == false && _enable)
                {
                    var item = new Item<(IWeapon, IWeaponInput)>(_itemData, _weapon, _itemView);
                    var slot = (new WeaponSelector(_playerRoot), item);
                    _inventory.Add(slot, 1);
                    gameObject.SetActive(false);
                    _enable = false;
                }
            }
        }
    }
}