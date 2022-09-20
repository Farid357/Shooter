using System.Collections.Generic;
using Shooter.GameLogic;
using Shooter.Model;
using Shooter.SaveSystem;
using Shooter.Tools;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Shooter.Root
{
    public sealed class EnemyRoot : CompositeRoot
    {
        [FormerlySerializedAs("_factories")] [SerializeField]
        private BulletsFactory[] _bulletsFactories;

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
        
        private SystemUpdate _systemUpdate;
        private WaveFactory _waveFactory;

        public override void Compose()
        {
            var wallet = new Wallet(_moneyView, new BinaryStorage());

            var abilities = new IAbility[]
            {
                new CharacterSpeedBoostAbility(_speedBoostAbility, _characterMovement, 6f),
                new CharacterIncreaseBulletsDamageAbility(_bulletsDamageAbility, FindObjectsOfType<BulletCollision>(), 6f),
                new CharacterHealthRegenerationAbility(_regenerationAbility, _characterHealthTransformView.Health)
            };

            IRewardFactory rewardFactory = new RandomRewardFactory(abilities, new IReward[] { new MoneyReward(wallet, 5) });
            _systemUpdate = new SystemUpdate();
            _enemyFactory.Init(_systemUpdate, rewardFactory, _scoreRoot.ComposeScore());
            _bulletsFactories.ForEach(factory => factory.Init(_systemUpdate));
            IEnemiesSimulation simulation = new EnemySimulation(_navMeshBaker);
            var waitNextWaveTimer = new Timer(_waveTimerSecondsView, 0.01f);
            _waveFactory = new WaveFactory(new EnemyWaves(simulation), waitNextWaveTimer, new WavesDataQueue(_wavesData.ToQueue()));
            _waveFactory.SpawnNextLoop().Forget();
            _systemUpdate.Add(waitNextWaveTimer);
        }

        private void Update() => _systemUpdate.Update(Time.deltaTime);
        
    }
}