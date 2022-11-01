using Cysharp.Threading.Tasks;
using Shooter.Model;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class TrashGolemAttack : SerializedMonoBehaviour, IEnemyAttack
    {
        [SerializeField] private EnemyToCharacterChaser _chaser;
        [SerializeField] private IAttackAnimation _attackAnimation;
        [SerializeField] private BulletMovement _prefab;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private EnemySound _enemyAttackSound;

        private IndependentPool<BulletMovement> _pool;
        private bool _isNotAttacking = true;

        private void OnEnable()
        {
            _pool ??= new IndependentPool<BulletMovement>(new GameObjectsFactory<BulletMovement>(_prefab, _bulletSpawnPoint.position));
        }

        private void Update()
        {
            if (_chaser.NearCharacter() && _isNotAttacking)
                Attack(_chaser.Character.Health).Forget();
        }

        private async UniTaskVoid Attack(IHealth character)
        {
            _isNotAttacking = false;
            _enemyAttackSound.Play();
            await _attackAnimation.Play();
            
            if (_chaser.NearCharacter() && character.IsAlive)
            {
                var bullet = _pool.Get();
                bullet.gameObject.SetActive(true);
                var direction = (_chaser.Character.Position - transform.position).normalized;
                bullet.Throw(direction);
            }

            _isNotAttacking = true;
        }
    }
}