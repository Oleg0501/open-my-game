using UnityEngine;

namespace Code.Logic.Blocks
{
    public static class BlockMovementDirectionHelper
    {
        public static BlockMovementDirection GetNormalizedDirection(Vector2 direction)
        {
            direction.Normalize();
            
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                return direction.x > 0 ? BlockMovementDirection.Right : BlockMovementDirection.Left;
            }

            return direction.y > 0 ? BlockMovementDirection.Up : BlockMovementDirection.Down;
        }
    }
}