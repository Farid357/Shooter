using System;
using Shooter.LoadSystem;

namespace Shooter.Model
{
    public sealed class LoadSceneButtonAction : IButtonClickAction
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly SceneData _loadScene;

        public LoadSceneButtonAction(ISceneLoader sceneLoader, SceneData loadScene)
        {
            _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
            _loadScene = loadScene ?? throw new ArgumentNullException(nameof(loadScene));
        }

        public void OnClick() => _sceneLoader.Load(_loadScene);
        
    }
}