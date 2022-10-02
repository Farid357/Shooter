using System.Collections.Generic;
using Shooter.GameLogic;
using Shooter.Model;
using Shooter.SaveSystem;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class EnemyRoot : CompositeRoot
    {
        [SerializeField] private StandartEnemyFactory _enemyFactory;
        [SerializeField] private ICharacterMovement _characterMovement;
        [SerializeField] private IView<int> _moneyView;
        [SerializeField] private IHealthTransformView _characterHealthTransformView;
        [SerializeField] private IAbilityView _speedBoostAbility;
        [SerializeField] private IAbilityView _bulletsDamageAbility;
        [SerializeField] private IAbilityView _regenerationAbility;
        [SerializeField] private INavMeshBaker _navMeshBaker;
        [SerializeField] private List<EnemyWaveData> _wavesData;
        [SerializeField] private IScoreRoot _scoreRoot;
        [SerializeField] private IView<float> _waveTimerSecondsView;
        [SerializeField] private IView<int> _diedEnemiesView;
        [SerializeField] private IEnergyShield _energyShield;
        [SerializeField] private List<IBulletsFactory> _bulletsFactories;
        
        private readonly SystemUpdate _systemUpdate = new();
        private WaveFactory _waveFactory;
        private CharacterIncreaseBulletsDamageAbility _characterIncreaseBulletsDamageAbility;

        public override void Compose()
        {
            var wallet = new Wallet(_moneyView, new BinaryStorage());

            _characterIncreaseBulletsDamageAbility = new CharacterIncreaseBulletsDamageAbility(_bulletsDamageAbility, _bulletsFactories.ToArray(), 6f);
            var abilities = new IAbility[]
            {
                new CharacterSpeedBoostAbility(_speedBoostAbility, _characterMovement, 6f),
                _characterIncreaseBulletsDamageAbility,
                new CharacterHealthRegenerationAbility(_regenerationAbility, _characterHealthTransformView.Health)
            };

            IRewardFactory rewardFactory = new RandomRewardFactory(abilities, new IReward[]
                {
                    new Rewards(new IReward[]
                    {
                        new MoneyReward(wallet, 5),
                        new DiedHealthsCounterReward(_diedEnemiesView)
                    }),
                    
                    new Rewards(new IReward[]
                    {
                        new EnergyShieldActivateReward(_energyShield),
                        new DiedHealthsCounterReward(_diedEnemiesView)
                    }),
                    
                    new Rewards(new IReward[]
                    {
                        new MoneyReward(wallet, 25),
                        new EnergyShieldActivateReward(_energyShield),
                        new DiedHealthsCounterReward(_diedEnemiesView)
                    }),
                }
            );

            _enemyFactory.Init(_systemUpdate, rewardFactory, _scoreRoot.ComposeScore());
            IEnemiesSimulation simulation = new EnemySimulation(_navMeshBaker);
            var waitNextWaveTimer = new Timer(_waveTimerSecondsView, 0.01f);
            _waveFactory = new WaveFactory(new EnemyWaves(simulation), waitNextWaveTimer, new WavesDataQueue(_wavesData.ToQueue()));
            _waveFactory.SpawnNextLoop().Forget();
            _systemUpdate.Add(waitNextWaveTimer);
        }

        private void Update() => _systemUpdate.Update(Time.deltaTime);

        private void OnDestroy() => _characterIncreaseBulletsDamageAbility.Dispose();
        
    }
}