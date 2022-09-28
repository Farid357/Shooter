using Shooter.GameLogic;
using UnityEngine;

namespace Shooter.Shop
{
    public sealed class GoodInContentView : GoodView
    {
        [field: SerializeField] public SelectingGoodButton SelectingButton { get; private set; }

    }
}