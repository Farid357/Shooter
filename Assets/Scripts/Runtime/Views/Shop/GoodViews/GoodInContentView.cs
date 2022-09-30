using System;
using Shooter.GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Shop
{
    public sealed class GoodInContentView : GoodView
    {
        [SerializeField] private Image _image;

        public SelectingGoodButton SelectingButton { get; private set; }

        public void Init(SelectingGoodButton selectingGoodButton)
        {
            SelectingButton = selectingGoodButton ?? throw new ArgumentNullException(nameof(selectingGoodButton));
        }

        protected override void VisualizeFeedback(GoodData goodData)
        {
            _image.sprite = goodData.Sprite;
        }
    }
}