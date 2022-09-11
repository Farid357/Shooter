using System;
using Shooter.GameLogic;
using Shooter.Model;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class BulletsPickup : MonoBehaviour, IBulletsPickup
{
    [SerializeField, Min(1)] private int _addBullets = 10;

    private IInventory<IWeapon> _inventory;

    protected abstract Type WeaponTypeForAddBullets { get; }

    public void Init(IInventory<IWeapon> inventory)
    {
        _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CharacterMovement>())
        {
            foreach (var item in _inventory.Items)
            {
                if (WeaponTypeForAddBullets == item.Object.GetType())
                {
                    item.Object.AddBullets(_addBullets);
                }
            }

            gameObject.SetActive(false);
        }
    }
}