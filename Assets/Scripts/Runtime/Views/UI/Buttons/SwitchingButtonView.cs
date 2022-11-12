using Shooter.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Root
{
    public sealed class SwitchingButtonView : MonoBehaviour, ISwitchingStateButtonActionView
    {
        [SerializeField] private Image _checkMark;
        
        public void Visualize(bool isOn)
        {
            _checkMark.gameObject.SetActive(isOn);
        }
    }
}