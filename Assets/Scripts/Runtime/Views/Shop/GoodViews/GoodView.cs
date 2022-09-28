using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Shop
{
    public abstract class GoodView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _nameText;
        
        public string Name => _nameText.text;
        
        public void Visualize(GoodData good)
        {
            _priceText.text = good.Price.ToString();
            _nameText.text = good.Name;
            _image.sprite = good.Sprite;
        }
    }
}