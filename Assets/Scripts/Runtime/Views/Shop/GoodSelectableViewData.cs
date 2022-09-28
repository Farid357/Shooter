using Shooter.GameLogic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Shop
{
    public sealed class GoodSelectableViewData : MonoBehaviour
    {
        [field: SerializeField, Required] public RemovingGoodButton RemovingButton { get; private set; }
        
        [field: SerializeField, Required] public SelectingGoodButton SelectingButton { get; private set; }
        
    }
}