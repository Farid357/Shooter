using System.Threading.Tasks;

namespace Shooter.Model.Inventory
{
    public interface IInventoryItemGameObjectView
    {
        Task Show();

        Task Hide();
    }
}