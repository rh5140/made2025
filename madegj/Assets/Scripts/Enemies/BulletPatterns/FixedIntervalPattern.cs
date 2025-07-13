using System;
using Projectiles;
using UnityEngine;

namespace Enemies.BulletPatterns
{
    public class FixedIntervalPattern : EnemyBulletPattern
    {
        [Serializable]
        private struct Pattern
        {
            public Projectile ProjectilePrefab;
            public int ProjectileCount;
            public int angleSpacing;
        }

        [SerializeField]
        private Pattern pattern;

        [SerializeField]
        private float interval;

        private float timer;

        private void Start()
        {
            timer = interval;
        }

        public override void UpdateBulletPattern(ProtagCore player1, ProtagCore player2, float deltaTime)
        {
            if (timer > 0)
            {
                timer -= deltaTime;
                return;
            }

            timer = interval;

            // closest player
            ProtagCore targetPlayer = player1;
            if (Vector3.Distance(transform.position, player2.GetPosition()) <
                Vector3.Distance(transform.position, player1.GetPosition()))
            {
                targetPlayer = player2;
            }

            // calculate angle to player
            Vector2 directionToPlayer = (targetPlayer.GetPosition() - (Vector2)transform.position).normalized;
            float baseAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            float startAngle = baseAngle - pattern.angleSpacing * (pattern.ProjectileCount - 1) / 2f;

            for (var i = 0; i < pattern.ProjectileCount; i++)
            {
                float angle = startAngle + i * pattern.angleSpacing;
                var spawnDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

                // Spawn projectile
                Projectile projectile = Instantiate(pattern.ProjectilePrefab, transform.position, Quaternion.identity);
                projectile.Initialize(spawnDirection);
            }
        }
    }
}