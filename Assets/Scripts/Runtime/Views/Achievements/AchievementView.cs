using System;
using Shooter.SaveSystem;
using UnityEngine;

namespace Shooter.Model
{
    public sealed class AchievementView : MonoBehaviour, IAchievementView
    {
        [SerializeField] private ParticleSystem _particle;
        
        private readonly IStorage _storage = new BinaryStorage();
        private AchievementGettingPanelView _panel;
        private string _path;
        private AchievementViewData _data;

        public void Init(AchievementViewData data, AchievementGettingPanelView panel)
        {
            _panel = panel ?? throw new ArgumentNullException(nameof(panel));
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _path = $"{data.name} {gameObject.name} {nameof(AchievementView)}";
            gameObject.SetActive(_storage.Exists(_path) ? _storage.Load<bool>(_path) : true);
        }
        
        public void VisualizeGetting()
        {
            _particle.Play();
            _panel.Show(_data.Name);
            _storage.Save(_path, true);
            gameObject.SetActive(false);
        }
    }
}