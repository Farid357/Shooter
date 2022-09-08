using System;
using System.Linq;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.GameLogic.Inventory
{
    public sealed class InventoryItemView : MonoBehaviour, IItemView
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _image;
        private ItemGameObjectView _itemGameObjectView;
        
        public void Init(ItemGameObjectView itemGameObjectView)
        {
            _itemGameObjectView = itemGameObjectView ?? throw new ArgumentNullException(nameof(itemGameObjectView));
        }
        
        public void Visualize(Sprite sprite, int count)
        {
            _text.text = count == 1 ? string.Empty : count.ToString();
            _image.sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));
        }
        
        public void Show() => _itemGameObjectView.Show();

        public void Hide() => _itemGameObjectView.Hide();
        
    }
}