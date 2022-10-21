using Shooter.Tools;
using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public sealed class ItemGameObjectViewFactory : MonoBehaviour, IFactory<IInventoryItemGameObjectView>
    {
        [SerializeField] private ItemGameObjectView _prefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _parent;
        [SerializeField] private bool _spawnPointRotation;
        
        public IInventoryItemGameObjectView Create()
        {
            var inventoryItemGameObjectView = Instantiate(_prefab, _spawnPoint.position, _spawnPointRotation ? _spawnPoint.rotation : Quaternion.identity, _parent);
            inventoryItemGameObjectView.gameObject.SetActive(false);
            return inventoryItemGameObjectView;
        }
    }
}