using UnityEngine;

namespace Shooter.Shop
{
    public abstract class GoodView : MonoBehaviour
    {
        private GoodData _good;

        public string Name => _good.Name;

        public void Visualize(GoodData good)
        {
            _good = good;
            VisualizeFeedback(good);
        }

        protected abstract void VisualizeFeedback(GoodData goodData);
    }
}