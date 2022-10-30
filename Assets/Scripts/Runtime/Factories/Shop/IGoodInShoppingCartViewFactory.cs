namespace Shooter.Shop
{
    public interface IGoodInShoppingCartViewFactory
    {
        IGoodView Create(IGood good);
    }
}