namespace Code.Scene.SpriteAnimator.Contracts
{
    public interface ISpriteAnimatorsRegistry
    {
        void Register(int id, Scene.SpriteAnimator.SpriteAnimator animator);
        void Unregister(int id);
        void Clear();
        void Tick(float deltaTime);
    }
}