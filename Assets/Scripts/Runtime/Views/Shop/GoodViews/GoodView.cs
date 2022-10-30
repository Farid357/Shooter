using UnityEngine;

namespace Shooter.Shop
{
    public abstract class GoodView : MonoBehaviour, IGoodView
    {
        private IGoodData _good;

        public string Name => _good?.Name;

        public void Visualize(IGoodData good)
        {
            _good = good;
            VisualizeFeedback(good);
        }

        public void Destroy() => Destroy(gameObject);

        protected abstract void VisualizeFeedback(IGoodData goodData);
    }
}