using System.Collections.Generic;
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class EnemiesInRadiusFinder : MonoBehaviour, IEnemiesInRadiusFinder
    {
        [SerializeField, Min(0.01f)] private float _radius = 5f;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1f, 0.87f, 0.25f);
            Gizmos.DrawSphere(transform.position, _radius);
        }

        public bool TryFind(out List<IEnemy> enemies)
        {
            var colliders = Physics.OverlapSphere(transform.position, _radius);
            enemies = new List<IEnemy>();

            for (var i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.TryGetComponent(out IEnemy enemy))
                {
                    enemies.Add(enemy);
                }
            }

            return enemies.Count > 0;
        }
    }
}