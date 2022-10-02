using System;
using System.ComponentModel;
using Shooter.Tools;

namespace Shooter.LoadSystem
{
    public sealed class SceneLoaderFactory : IFactory<ISceneLoader>
    {
        private readonly SceneLoadMode _sceneLoadMode;
        private readonly IScreenFade _screenFade;
        private readonly SceneData _loaderScene;
        
        public SceneLoaderFactory(SceneLoadMode sceneLoadMode, IScreenFade screenFade, SceneData loaderScene)
        {
            if (!Enum.IsDefined(typeof(SceneLoadMode), sceneLoadMode))
                throw new InvalidEnumArgumentException(nameof(sceneLoadMode), (int)sceneLoadMode, typeof(SceneLoadMode));
            
            _sceneLoadMode = sceneLoadMode;
            _screenFade = screenFade ?? throw new ArgumentNullException(nameof(screenFade));
            _loaderScene = loaderScene ?? throw new ArgumentNullException(nameof(loaderScene));
        }
        

        public ISceneLoader Create()
        {
            return _sceneLoadMode switch
            {
                SceneLoadMode.Simple => new StandartSceneLoader(),
                SceneLoadMode.WihtFadeScreen => new SceneLoaderWithScreenFade(_screenFade),
                SceneLoadMode.WithLoadScreen => new SceneLoaderWithScreen(_loaderScene),
                _ => throw new ArgumentOutOfRangeException(nameof(_sceneLoadMode))
            };
        }
    }
}