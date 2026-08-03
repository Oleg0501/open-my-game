using System;
using System.Collections.Generic;
using System.Linq;
using Code.Logic.LevelBlock;
using Code.Scene.Block.Contracts;

namespace Code.Scene.Block.Implementations
{
    public class BlockViewsRegistry : IBlockViewsRegistry
    {
        private readonly Dictionary<BlockID, BlockView> _views = new();
        
        public BlockView Register(BlockID blockID, BlockView view)
        {
            _views.Add(blockID, view);
            
            return view;
        }

        public BlockView Unregister(BlockID blockID)
        {
            var view = _views[blockID];
            _views.Remove(blockID);
            
            return view;
        }

        public void Clear()
        {
            _views.Clear();
        }

        public BlockView GetView(BlockID blockID)
        {
            return _views.TryGetValue(blockID, out var view) 
                ? view 
                : throw new Exception($"Block view with ID '{blockID}' not registered");
        }

        public BlockView[] GetViewsAll()
        {
            return _views.Values.ToArray();
        }
    }
}