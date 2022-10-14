using System.Collections.Generic;
using Shooter.GameLogic;
using Shooter.Model;
using Shooter.SaveSystem;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class AchievementRoot : CompositeRoot
    {
        [SerializeField] private ScoreRoot _scoreRoot;
        [SerializeField] private AchievementGettingPanelView _achievementPanel;
        [SerializeField] private Transform _content;
        [SerializeField] private AchievementFactory _achievementFactory;
        [SerializeField] private AchievementEnemiesCounterData[] _achievementsEnemiesCounterData;
        [SerializeField] private AchievementMoneyCounterData[] _achievementsMoneyCounterData;
        [SerializeField] private AchievementView _prefab;
        private readonly List<IAchievement> _achievements = new();

        private void Update()
        {
            for (var i = 0; i < _achievements.Count; i++)
            {
                var achievement = _achievements[i];
                
                if (achievement.CanGet())
                {
                    achievement.Get();
                }
            }
        }

        public override void Compose()
        {
            foreach (var achievementData in _achievementsEnemiesCounterData)
            {
                var achievementView = CreateView(achievementData);
                _achievements.Add(new CountAchievement(_scoreRoot.Score, new Achievement(achievementView, new DummyReward()), achievementData.NeedAmount));
            }

            foreach (var achievementData in _achievementsMoneyCounterData)
            {
                var achievementView = CreateView(achievementData);
                //Change
                _achievements.Add(new CountAchievement(_scoreRoot.Score, new Achievement(achievementView, new MoneyReward(new Wallet(new DummyCountView(), new BinaryStorage()), achievementData.MoneyReward))
                    , achievementData.NeedAmount));
            }
        }

        private AchievementView CreateView(AchievementCounterData achievementData)
        {
            var achievementView = Instantiate(_prefab, _content);
            achievementView.Init(achievementData.ViewData, _achievementPanel);
            return achievementView;
        }
    }
}