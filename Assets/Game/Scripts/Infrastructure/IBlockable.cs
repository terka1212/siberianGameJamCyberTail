namespace Game.Infrastructure.ScopedLifecycle
{
    public interface IBlockable
    {
        public void Block();
        public void Unblock();
    }
}