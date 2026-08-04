namespace Code.Scene.Core.Contracts
{
    public interface ITickableRegistry
    {
        void Register(int id, ITickable tickable);
        void Unregister(int id);
        void Clear();
        void Tick(float deltaTime);
    }
}