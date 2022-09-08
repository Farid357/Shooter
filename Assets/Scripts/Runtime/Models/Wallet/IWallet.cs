namespace Shooter.Model
{
    public interface IWallet
    {
        public void Put(int money);

        public bool CanTake(int money);

        public void Take(int money);
    }
}