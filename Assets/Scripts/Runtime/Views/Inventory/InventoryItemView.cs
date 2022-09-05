using System;
using Shooter.Model.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.GameLogic.Inventory
{
    public sealed class InventoryItemView : MonoBehaviour, IItemView
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _image;

        public void Visualize(Sprite sprite, int count)
        {
            _text.text = count == 1 ? string.Empty : count.ToString();
            _image.sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));
        }
        
        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
        
    }
}