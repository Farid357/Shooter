using System.Threading.Tasks;
using Shooter.Model.Inventory;
using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public sealed class InventoryItemGameObjectView : MonoBehaviour, IInventoryItemGameObjectView
    {
        public async Task Show()
        {
            gameObject.SetActive(true);
            await Task.Yield();
        }

        public async Task Hide()
        {
            gameObject.SetActive(false);
            await Task.Yield();
        }
    }
}