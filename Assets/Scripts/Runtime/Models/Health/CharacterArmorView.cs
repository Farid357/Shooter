using Shooter.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.GameLogic
{
    public sealed class CharacterArmorView : MonoBehaviour, IArmorView
    {
        [SerializeField] private Scrollbar _scrollbar;
        
        public void Visualize(int protection)
        {
            _scrollbar.gameObject.SetActive(protection != 0);
            _scrollbar.value = protection / 100f;
        }
    }
}