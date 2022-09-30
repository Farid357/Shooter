namespace Shooter.Shop
{
    public interface INotEnoughMoneyView
    {
        void Visualize(int needMoney, int currentMoney);
    }
}