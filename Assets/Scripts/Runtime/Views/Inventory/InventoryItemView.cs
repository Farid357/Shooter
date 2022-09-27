using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.GameLogic.Inventory
{
    public sealed class InventoryItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _image;

        public void Visualize(Sprite sprite, int count)
        {
            _text.text = count == 0 ? string.Empty : count.ToString();
            _image.sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));
        }
    }
}