using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemies.Movement
{
    public class MoveTowardsPlayer : EnemyMovement
    {
        [Header("Config")]

        [SerializeField]
        private float speed;

        [SerializeField]
        private float acceleration;

        [Header("Tracking")]

        [SerializeField]
        private float trackingWeight;

        [Header("Flocking")]

        [SerializeField]
        private float avoidanceWeight;

        public override void CalculateMovement(ProtagCore player1, ProtagCore player2, float timeDelta)
        {
            // find nearest player
            ProtagCore targetPlayer = player1;
            if (Vector3.Distance(transform.position, player2.GetPosition()) <
                Vector3.Distance(transform.position, player1.GetPosition()))
            {
                targetPlayer = player2;
            }

            Vector2 finalDirection = Vector2.zero;

            Vector2 trackingDirection = (targetPlayer.GetPosition() - (Vector2)transform.position).normalized;

            finalDirection += trackingDirection * trackingWeight;

            // Avoidance
            Vector2 avoidanceDirection = Vector2.zero;
            if (WaveManager.Instance)
            {
                IReadOnlyList<EnemyCore> allEnemies = WaveManager.Instance.GetLivingEnemies();

                int enemyCount = allEnemies.Count() - 1;
                enemyCount = Mathf.Max(enemyCount, 1);

                foreach (EnemyCore enemy in allEnemies)
                {
                    Vector2 directionToEnemy = (enemy.transform.position - transform.position).normalized;
                    float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

                    distanceToEnemy = Mathf.Max(distanceToEnemy, 0.2f);

                    avoidanceDirection -= directionToEnemy / distanceToEnemy;
                }

                avoidanceDirection /= enemyCount;
            }

            finalDirection += avoidanceDirection * avoidanceWeight;

            // Final velocity
            Vector2 desiredVelocity = finalDirection.normalized * speed;
            Vector2 newVelocity = Vector2.MoveTowards(
                rigidbody2D.linearVelocity,
                desiredVelocity,
                acceleration * timeDelta);

            rigidbody2D.linearVelocity = newVelocity;

            // Rotate
            if (newVelocity != Vector2.zero)
            {
                float angle = Mathf.Atan2(trackingDirection.y, trackingDirection.x) * Mathf.Rad2Deg;
                rigidbody2D.rotation = angle;
            }
        }
    }
}