using Enemies.BulletPatterns;
using Enemies.Movement;
using UnityEngine;
using UnityEngine.Events;

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

        public UnityEvent OnDefeated;
        public UnityEvent OnRemoveCorpse;

        private void Update()
        {
            if (enemyMovement == null || enemyBulletPattern == null || player1 == null || player2 == null)
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

        public void CleanupCorpse()
        {
            OnRemoveCorpse?.Invoke();
            Destroy(gameObject, 2f);
        }

        public void Defeat()
        {
            OnDefeated?.Invoke();
        }
    }
}