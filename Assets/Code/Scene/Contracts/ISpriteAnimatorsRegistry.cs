namespace Code.Scene.Contracts
{
    public interface ISpriteAnimatorsRegistry
    {
        void Register(int id, SpriteAnimator animator);
        void Unregister(int id);
        void Clear();
        void Tick(float deltaTime);
    }
}