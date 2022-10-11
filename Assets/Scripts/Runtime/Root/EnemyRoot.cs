using System.Collections.Generic;
using Shooter.GameLogic;
using Shooter.Model;
using Shooter.SaveSystem;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class EnemyRoot : CompositeRoot
    {
        [Title("Views")] [SerializeField] private IView<float> _waveTimerSecondsView;
        [SerializeField] private IView<int> _diedEnemiesView;
        [SerializeField] private IView<int> _moneyView;
        [SerializeField] private IView<int> _aliveEnemiesView;

        [Title("Factories")] [SerializeField] private StandartEnemyFactory _enemyFactory;
        [SerializeField] private List<IBulletsFactory> _bulletsFactories;

        [Title("Character")] [SerializeField] private ICharacterMovement _characterMovement;
        [SerializeField] private IHealthTransformView _characterHealthTransformView;

        [Title("Abilities")] [SerializeField] private IAbilityView _speedBoostAbility;
        [SerializeField] private IAbilityView _bulletsDamageAbility;
        [SerializeField] private IAbilityView _regenerationAbility;

        [Title("Other", titleAlignment: TitleAlignments.Centered)] [SerializeField]
        private INavMeshBaker _navMeshBaker;

        [SerializeField] private IScoreRoot _scoreRoot;
        [SerializeField] private IEnergyShield _energyShield;
        [SerializeField] private List<EnemyWaveData> _wavesData;

        private readonly SystemUpdate _systemUpdate = new();
        private WaveFactory _waveFactory;
        private CharacterIncreaseBulletsDamageAbility _characterIncreaseBulletsDamageAbility;

        public IWaveFactory WaveFactory => _waveFactory;

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
                    new MoneyReward(wallet, 5),
                    new EnergyShieldActivateReward(_energyShield),

                    new Rewards(new IReward[]
                    {
                        new MoneyReward(wallet, 25),
                        new EnergyShieldActivateReward(_energyShield),
                    }),
                }
            );

            _enemyFactory.Init(_systemUpdate, rewardFactory, _scoreRoot.ComposeScore(), new DiedHealthsCounterReward(_diedEnemiesView));
            var simulation = new EnemySimulation(_navMeshBaker, _aliveEnemiesView);
            var waitNextWaveTimer = new Timer(_waveTimerSecondsView, 0.01f);
            _waveFactory = new WaveFactory(new EnemyWaves(simulation), waitNextWaveTimer, new WavesDataQueue(_wavesData.ToQueue()));
            _waveFactory.SpawnNextLoop().Forget();
            _systemUpdate.Add(waitNextWaveTimer, simulation);
        }

        private void Update() => _systemUpdate.Update(Time.deltaTime);

        private void OnDestroy() => _characterIncreaseBulletsDamageAbility.Dispose();
    }
}