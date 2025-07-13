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

        public UnityEvent<EnemyCore> OnDefeated;
        public UnityEvent OnRemoveCorpse;

        public bool IsDefeated => defeated;

        private bool defeated;

        private void Update()
        {
            if (enemyMovement == null || enemyBulletPattern == null || player1 == null || player2 == null)
            {
                return;
            }

            // Update movement
            enemyMovement.CalculateMovement(player1, player2, Time.deltaTime);

            // Update bullet pattern
            enemyBulletPattern.UpdateBulletPattern(player1, player2, Time.deltaTime);
        }

        public void Initialize(ProtagCore player1, ProtagCore player2)
        {
            this.player1 = player1;
            this.player2 = player2;
            defeated = false;
        }

        public void CleanupCorpse()
        {
            OnRemoveCorpse?.Invoke();
            Destroy(gameObject, 2f);
        }

        public void Defeat()
        {
            if (defeated)
            {
                return;
            }

            defeated = true;
            OnDefeated?.Invoke(this);
        }
    }
}