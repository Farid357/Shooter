using System.Collections.Generic;
using Shooter.GameLogic;
using Shooter.Model;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class EnemyRoot : CompositeRoot
    {
        [Title("Views")] 
        [SerializeField] private IView<float> _waveTimerSecondsView;
        [SerializeField] private IView<int> _diedEnemiesView;
        [SerializeField] private IView<int> _aliveEnemiesView;
        
        [Title("Character")] 
        [SerializeField] private ICharacterMovement _characterMovement;
        [SerializeField] private IHealthTransformView _characterHealthTransformView;
        
        [Title("Other", titleAlignment: TitleAlignments.Centered)] 
        [SerializeField] private INavMeshBaker _navMeshBaker;
        [SerializeField] private IAbilityRoot _abilityRoot;
        [SerializeField] private IWalletRoot _walletRoot;
        [SerializeField] private IScoreRoot _scoreRoot;
        [SerializeField] private IEnergyShield _energyShield;
        [SerializeField] private List<EnemyWaveData> _wavesData;
        [SerializeField] private bool _needSelectRandomWeaponOnEnemyDied;
        [SerializeField] private PickupsRoot _pickupsRoot;
        [SerializeField] private IPlayerRoot _playerRoot;
        
        private readonly SystemUpdate _systemUpdate = new();
        private WaveFactory _waveFactory;
        private EnemySimulation _enemySimulation;

        public IWaveFactory WaveFactory => _waveFactory;

        public override void Compose()
        {
            var wallet = _walletRoot.CoinsWallet();

            IRewardFactory rewardFactory = new RandomRewardFactory(_abilityRoot.Abilities(), new IReward[]
                {
                    new MoneyReward(wallet, 5),
                    new EnergyShieldActivateReward(_energyShield),
                    new MoneyReward(_walletRoot.DiamondsWallet(), 1),
                    
                    new Rewards(new IReward[]
                    {
                        new MoneyReward(wallet, 25),
                        new EnergyShieldActivateReward(_energyShield),
                    }),
                }
            );

            FindObjectsOfType<StandartEnemyFactory>().ForEach(factory => factory.Init(_systemUpdate, rewardFactory, _scoreRoot.Score(), new DiedHealthsCounter(_diedEnemiesView)));
            _enemySimulation = new EnemySimulation(_navMeshBaker, _aliveEnemiesView);
            var waitNextWaveTimer = new Timer(_waveTimerSecondsView, 0.01f);
            _waveFactory = new WaveFactory(new EnemyWaves(_enemySimulation), waitNextWaveTimer, new WavesDataQueue(_wavesData.ToQueue()));
            _waveFactory.SpawnNextLoop().Forget();
            if (_needSelectRandomWeaponOnEnemyDied)
            {
                var randomWeaponFactory = new RandomWeaponFactory(new (IWeapon, IWeaponInput)[]
                {
                    (new WeaponWithShotWaiting(new Weapon(_pickupsRoot.LaserData.BulletsFactory, _pickupsRoot.LaserData.BulletsView, _pickupsRoot.LaserData.Bullets), _pickupsRoot.LaserData.WaitSeconds),
                         new StandartWeaponInput())
                });
                
                var randomWeaponSelector = new RandomWeaponSelector(_enemySimulation, randomWeaponFactory, _playerRoot);
                _systemUpdate.Add(randomWeaponSelector);
            }
            _systemUpdate.Add(waitNextWaveTimer, _enemySimulation);
        }

        private void Update() => _systemUpdate.Update(Time.deltaTime);

        private void LateUpdate() => _enemySimulation.LateUpdate(Time.deltaTime);
        
    }
}