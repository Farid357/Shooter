using Cysharp.Threading.Tasks;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnemySound : MonoBehaviour
    {
        [SerializeField] private AudioSource _prefab;

        public void Play()
        {
            var audio = Instantiate(_prefab);
            audio.Play();
            audio.DestroyOnEnded().Forget();
        }
    }
}