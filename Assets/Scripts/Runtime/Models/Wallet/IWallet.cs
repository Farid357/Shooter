namespace Shooter.Model
{
    public interface IWallet
    {
        public int Money { get; }

        public void Put(int money);

        public bool CanTake(int money);

        public void Take(int money);
    }
}