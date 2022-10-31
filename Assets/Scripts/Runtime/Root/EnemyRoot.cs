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

        private readonly SystemUpdate _systemUpdate = new();
        private WaveFactory _waveFactory;

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
            var simulation = new EnemySimulation(_navMeshBaker, _aliveEnemiesView);
            var waitNextWaveTimer = new Timer(_waveTimerSecondsView, 0.01f);
            _waveFactory = new WaveFactory(new EnemyWaves(simulation), waitNextWaveTimer, new WavesDataQueue(_wavesData.ToQueue()));
            _waveFactory.SpawnNextLoop().Forget();
            _systemUpdate.Add(waitNextWaveTimer, simulation);
        }

        private void Update() => _systemUpdate.Update(Time.deltaTime);
    }
}