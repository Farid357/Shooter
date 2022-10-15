using System.Collections.Generic;
using Shooter.GameLogic;
using Shooter.Model;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class AchievementRoot : CompositeRoot
    {
        [SerializeField] private IWalletRoot _walletRoot;
        [SerializeField] private IScoreRoot _scoreRoot;
        [SerializeField] private AchievementGettingPanelView _achievementPanel;
        [SerializeField] private Transform _content;
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
                _achievements.Add(new CountAchievement(_scoreRoot.Score(),
                    new Achievement(CreateView(achievementData), new DummyReward()), achievementData.NeedAmount));
            }

            foreach (var achievementData in _achievementsMoneyCounterData)
            {
                _achievements.Add(new CountAchievement(_scoreRoot.Score(),
                    new Achievement(CreateView(achievementData), 
                        new MoneyReward(_walletRoot.Wallet(), achievementData.MoneyReward)), achievementData.NeedAmount));
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