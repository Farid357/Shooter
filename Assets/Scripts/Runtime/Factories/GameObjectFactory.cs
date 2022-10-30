using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public abstract class GameObjectFactory<TPrefab, TParent> : SerializedMonoBehaviour, IFactory<TPrefab> where TPrefab : MonoBehaviour where TParent : MonoBehaviour
    {
        [SerializeField] private TPrefab _prefab;
        [SerializeField] private TParent _parent;
        [SerializeField] private Transform _spawnPoint;
        
        public TPrefab Create()
        {
            var obj = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity, _parent.transform);
            obj.gameObject.SetActive(false);
            return obj;
        }
    }
}