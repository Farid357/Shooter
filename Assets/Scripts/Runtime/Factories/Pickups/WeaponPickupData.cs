using System;
using Shooter.GameLogic.Inventory;
using UnityEngine;

namespace Shooter.GameLogic
{
    [Serializable]
    public struct WeaponPickupData
    {
        [field: SerializeField] public WeaponPickup Pickup { get; private set; }
        
        [field: SerializeField] public ItemGameObjectViewFactory GameObjectViewFactory { get; private set; }
    }
}