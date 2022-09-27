using System.Threading.Tasks;

namespace Shooter.GameLogic.Inventory
{
    public interface IInventoryItemGameObjectView
    {
        public Task Show();

        public Task Hide();
    }
}