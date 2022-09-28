using Shooter.GameLogic;
using UnityEngine;

namespace Shooter.Shop
{
    public sealed class GoodInShoppingCartView : GoodView
    {
        [field: SerializeField] public RemovingGoodButton RemovingButton { get; private set; }

    }
}