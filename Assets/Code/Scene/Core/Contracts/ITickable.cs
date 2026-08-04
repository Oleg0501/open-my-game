namespace Code.Scene.Core.Contracts
{
    public interface ITickable
    {
        void Tick(float deltaTime);
    }
}