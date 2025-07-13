using System;
using Enemies.BulletPatterns;
using Enemies.Movement;
using UnityEngine;

namespace Enemies
{
    public class EnemyCore : MonoBehaviour
    {
        [SerializeField]
        private EnemyMovement enemyMovement;
        
        [SerializeField]
        private EnemyBulletPattern enemyBulletPattern;
        
        [SerializeField]
        private Transform player1;
        
        [SerializeField]
        private Transform player2;
        
        public void Initialize(Transform player1, Transform player2)
        {
            this.player1 = player1;
            this.player2 = player2;

        }

        private void Update()
        {
            if (enemyMovement == null || enemyBulletPattern == null)
            {
                return;
            }

            // Update movement
            enemyMovement.UpdateMovement(player1, player2, Time.deltaTime);

            // Update bullet pattern
            enemyBulletPattern.UpdateBulletPattern(player1, player2, Time.deltaTime);
        }
    }
}
