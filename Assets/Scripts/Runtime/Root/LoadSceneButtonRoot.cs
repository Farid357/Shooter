using Shooter.GameLogic;
using Shooter.LoadSystem;
using Shooter.Model;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class LoadSceneButtonRoot : CompositeRoot
    {
        [SerializeField] private LoadSceneButton _button;
        [SerializeField] private SceneData _scene;
        [SerializeField] private SceneLoader _sceneLoader;
        
        public override void Compose()
        {
            var loadSceneButtonAction = new LoadSceneButtonAction(_sceneLoader, _scene);
            _button.Subscribe(loadSceneButtonAction);
        }
    }
}