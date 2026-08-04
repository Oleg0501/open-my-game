using Code.Scene.Block.Config;
using Code.Scene.Block.Contracts;
using Code.Scene.Core;
using UnityEngine;
using Zenject;

namespace Code.Scene.Block.Implementations
{
    public class BlockViewsScaleService : IBlockViewsScaleService
    {
        private readonly Camera _camera;
        private readonly BlockViewScaleConfig _config;

        [Inject]
        public BlockViewsScaleService([Inject(Id = typeof(CameraInjectID))] Camera camera, BlockViewScaleConfig config)
        {
            _camera = camera;
            _config = config;
        }

        public float GetSceneRootScale(float width, float height)
        {
            var worldHeight = _camera.orthographicSize * 2f;
            var worldWidth = worldHeight * _camera.aspect;
            
            var availableWidth = worldWidth - _config.HorizontalPadding * 2f;
            
            var scaleByWidth = availableWidth / width;
            var scaleByHeight = worldHeight / height;
            var scale = Mathf.Min(scaleByWidth, scaleByHeight);

            return Mathf.Min(scale, _config.MaxScale);
        }
    }
}