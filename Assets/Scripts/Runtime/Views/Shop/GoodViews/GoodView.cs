using UnityEngine;

namespace Shooter.Shop
{
    public abstract class GoodView : MonoBehaviour
    {
        private IGoodData _good;

        public string Name => _good.Name;

        public void Visualize(IGoodData good)
        {
            _good = good;
            VisualizeFeedback(good);
        }

        protected abstract void VisualizeFeedback(IGoodData goodData);
    }
}