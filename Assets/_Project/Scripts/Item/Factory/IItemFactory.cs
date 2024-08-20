namespace Item.Factory
{
    public interface IItemFactory
    {
        IItem Get(ItemType type);
        void Release(IItem item);
    }
}
