using Shooter.GameLogic;
using TMPro;
using UnityEngine;

namespace Shooter.Shop
{
    public sealed class GoodInShoppingCartView : GoodView
    {
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _nameText;

        [field: SerializeField] public RemovingGoodButton RemovingButton { get; private set; }

        protected override void VisualizeFeedback(GoodData goodData)
        {
            _priceText.text = goodData.Price.ToString();
            _nameText.text = goodData.Name;
        }
    }
}