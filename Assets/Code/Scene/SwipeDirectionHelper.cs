using Code.Logic.LevelBlock;
using UnityEngine;

namespace Code.Scene
{
    public static class SwipeDirectionHelper
    {
        public static BlockMoveDirection GetNormalizedDirection(Vector2 inputDirection)
        {
            inputDirection.Normalize();
            
            if (Mathf.Abs(inputDirection.x) > Mathf.Abs(inputDirection.y))
            {
                return inputDirection.x > 0 ? BlockMoveDirection.Right : BlockMoveDirection.Left;
            }

            return inputDirection.y > 0 ? BlockMoveDirection.Up : BlockMoveDirection.Down;
        }
    }
}