using System.Threading.Tasks;
using Shooter.GameLogic.Inventory;

public sealed class DummyItemView : IGameObjectItemView
{
    public async Task Show()
    {
        await Task.Yield();
    }

    public async Task Hide()
    {
        await Task.Yield();
    }
}