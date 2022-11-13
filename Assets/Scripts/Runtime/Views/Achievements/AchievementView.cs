using System;
using Shooter.Model;
using Shooter.SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.GameLogic
{
    public sealed class AchievementView : MonoBehaviour, IAchievementView
    {
        [SerializeField] private ParticleSystem _particlePrefab;
        [SerializeField] private Image _lock;
        [SerializeField] private Image _checkmark;
        [SerializeField] private TMP_Text _text;
        
        private readonly IStorage _storage = new BinaryStorage();
        private string _path;
        
        private AchievementGettingPanelView _panel;
        private AchievementViewData _data;

        public void Init(AchievementViewData data, AchievementGettingPanelView panel)
        {
            _panel = panel ?? throw new ArgumentNullException(nameof(panel));
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _text.text = _data.Name;
            _path = $"{data.Name} {data.Name} {gameObject.name} {nameof(AchievementView)}";
            var isOpen = _storage.Exists(_path) ? _storage.Load<bool>(_path) : false;
            _lock.gameObject.SetActive(!isOpen);
            _checkmark.gameObject.SetActive(isOpen);
        }
        
        public void VisualizeGetting()
        {
            Instantiate(_particlePrefab, transform.position, Quaternion.identity).Play();
            _panel.Show();
            _storage.Save(_path, true);
            _checkmark.gameObject.SetActive(true);
            _lock.gameObject.SetActive(false);
        }
    }
}