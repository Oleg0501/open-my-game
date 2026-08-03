using System;
using UnityEngine;

namespace Code.Logic.LevelBlock.Config
{
    [Serializable]
    public class BlockMatchConfig
    {
        [SerializeField] private Vector2Int[] _matchPoints;
        public Vector2Int[] MatchPoints => _matchPoints;
    }
}