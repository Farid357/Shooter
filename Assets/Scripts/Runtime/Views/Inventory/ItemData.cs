using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Create/Item")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        
        [field: SerializeField] public string Name { get; private set; }

    }
}