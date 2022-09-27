using System.Threading.Tasks;
using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public sealed class ItemGameObjectView : MonoBehaviour, IInventoryItemGameObjectView
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