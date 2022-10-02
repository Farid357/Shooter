using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.LoadSystem
{
    public sealed class SceneLoader : MonoBehaviour, ISceneLoader
    {
        [SerializeField] private SceneLoadMode _mode;

        [SerializeField, ShowIf("_mode", SceneLoadMode.WihtFadeScreen)]
        private ScreenFade _screen;

        [SerializeField, ShowIf("_mode", SceneLoadMode.WithLoadScreen)]
        private SceneData _loaderScene;
        
        public void Load(SceneData sceneData)
        {
            IFactory<ISceneLoader> factory = new SceneLoaderFactory(_mode, _screen, _loaderScene);
            var sceneLoader = factory.Create();
            sceneLoader.Load(sceneData);
        }
    }
}