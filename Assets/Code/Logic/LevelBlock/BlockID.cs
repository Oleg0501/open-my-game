using System;

namespace Code.Logic.LevelBlock
{
    public readonly struct BlockID : IEquatable<BlockID>
    {
        public readonly int Value;

        public BlockID(int value)
        {
            Value = value;
        }

        public override string ToString() => Value.ToString();

        public bool Equals(BlockID other) => Value == other.Value;

        public override int GetHashCode() => Value;

        public static implicit operator int(BlockID id) => id.Value;
    }
}