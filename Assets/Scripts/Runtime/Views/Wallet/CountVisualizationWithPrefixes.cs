using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class CountVisualizationWithPrefixes : MonoBehaviour, IView<int>
    {
        [SerializeField] private LineVisualization _lineVisualization;
        private readonly DigitsFormatter _digitsFormatter = new();

        public void Visualize(int count)
        {
            var text = _digitsFormatter.TryFormat(count);
           _lineVisualization.Visualize(text);
        }
    }
}