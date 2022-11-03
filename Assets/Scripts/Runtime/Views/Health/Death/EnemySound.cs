using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnemySound : MonoBehaviour
    {
        [SerializeField] private AudioSource _prefab;

        public async UniTaskVoid Play()
        {
            var audio = Instantiate(_prefab);
            audio.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(audio.clip.length));
            Destroy(audio.gameObject);
        }
    }
}