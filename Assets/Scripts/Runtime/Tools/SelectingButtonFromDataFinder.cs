using System.Linq;
using Shooter.GameLogic;
using UnityEngine;

namespace Shooter.Shop
{
    public sealed class SelectingButtonFromDataFinder :ISelectingButtonFromDataFinder
    {
        public IButton Find(GoodData data)
        {
            return Object.FindObjectsOfType<GoodInContentView>().ToList().Find(good => good.Name == data.Name).SelectingButton;
        }
    }
}