using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Shooter.Shop
{
    public sealed class NotEnoughMoneyView : MonoBehaviour, INotEnoughMoneyView
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField, Min(0.2f)] private float _showSeconds = 1.2f;
        
        public async void Visualize(int needMoney, int currentMoney)
        {
            _text.text = $"You don't have enough money! You need {needMoney} money, but you have {currentMoney}!";
            await UniTask.Create(async () =>
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_showSeconds));
                _text.text = string.Empty;
            });
        }
    }
}