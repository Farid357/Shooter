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
            
            if (audio != null)
                Object.Destroy(audio.gameObject);
        }

        public static float ToVolume(this float value)
        {
            return Mathf.Lerp(-30, 20, value);
        }
    }
}