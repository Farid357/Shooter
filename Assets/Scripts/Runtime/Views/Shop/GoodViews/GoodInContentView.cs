using DG.Tweening;
using Shooter.GameLogic;
using Shooter.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Shop
{
    public sealed class GoodInContentView : GoodView, IGoodUsingView
    {
        [SerializeField] private Image _image;
        [SerializeField] private Image _usingCheckmark;
        [SerializeField, Min(0.1f)] private float _usingFade = 0.25f;
        private readonly IStorage _storage = new BinaryStorage();

        [field: SerializeField] public SelectingGoodButton SelectingButton { get; private set; }

        public void VisualizeUsing(IGoodData goodData)
        {
            _storage.Save(CreateKey(goodData), true);
            _usingCheckmark.gameObject.SetActive(true);
            _image.DOFade(_usingFade, 0.01f);
        }
        
        protected override void VisualizeFeedback(IGoodData goodData)
        {
            _image.sprite = goodData.Sprite;
            _usingCheckmark.gameObject.SetActive(false);
            _image.DOFade(1, 0);
            
            if (HasUsed(goodData))
            {
                VisualizeUsing(goodData);
            }
        }
        
        private bool HasUsed(IGoodData good)
        {
            var key = CreateKey(good);
            return _storage.Exists(key) && _storage.Load<bool>(key);
        }
        
        private string CreateKey(IGoodData goodData) => $"{goodData.Name} {goodData.Price} {goodData.Sprite.name} {goodData.Name} 1";
        
    }
}