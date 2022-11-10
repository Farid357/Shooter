using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Shooter.Tools
{
    public static class AudioSourceUtils
    {
        public static async UniTaskVoid DestroyOnEnded(this AudioSource audio)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(audio.clip.length));
            Object.Destroy(audio.gameObject);
        }
    }
}