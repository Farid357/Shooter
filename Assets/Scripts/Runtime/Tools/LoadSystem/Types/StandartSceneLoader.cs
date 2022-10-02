using UnityEngine.SceneManagement;

namespace Shooter.LoadSystem
{
    public sealed class StandartSceneLoader : ISceneLoader
    {
        public void Load(SceneData sceneData) => SceneManager.LoadSceneAsync(sceneData.name);
        
    }
}
