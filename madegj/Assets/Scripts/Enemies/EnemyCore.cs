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
        private ProtagCore player1;

        [SerializeField]
        private ProtagCore player2;

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

        public void Initialize(ProtagCore player1, ProtagCore player2)
        {
            this.player1 = player1;
            this.player2 = player2;
        }
    }
}