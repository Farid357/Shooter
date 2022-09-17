using System.Threading.Tasks;

namespace Shooter.GameLogic.Inventory
{
    public interface IGameObjectItemView
    {
        public Task Show();

        public Task Hide();
    }
}