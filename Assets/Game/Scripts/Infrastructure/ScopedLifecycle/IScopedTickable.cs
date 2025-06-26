namespace Game.Infrastructure.ScopedLifecycle
{
    /// <summary>
    /// You need to Register object, that uses this interface, in ScopedLifecycleManager
    /// And you need to UnRegisterObject in Disposable method (IDisposable)
    /// </summary>
    public interface IScopedTickable
    {
        void ScopedTick();
    }
}