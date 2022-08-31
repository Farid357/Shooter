using System.Collections.Generic;
using System.Linq;
using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.GameLogic
{
    public sealed class HealthView : MonoBehaviour, IView<int>
    {
        [SerializeField] private Scrollbar _bar;
        [SerializeField, TableList] private List<ScreenBloodData> _screenBloodDatas;
        [SerializeField] private Image _blood;

        private void OnValidate()
        {
            if (_screenBloodDatas.Any(data => data.Sprite is null))
            {
                Debug.LogError("Sprite have to be not null!");
            }
        }

        public void Visualize(int health)
        {
            _bar.value = health / 100f;
            var data = _screenBloodDatas.Find(bloodData => bloodData.NeedHealthForSprite <= health);
            _blood.sprite = data.Sprite;
        }
    }
}