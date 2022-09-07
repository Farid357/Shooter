using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public sealed class ItemGameObjectView : MonoBehaviour, IItemView
    {
        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
        
    }
}