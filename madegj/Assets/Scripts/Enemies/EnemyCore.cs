using Enemies.BulletPatterns;
using Enemies.Movement;
using UnityEngine;
using UnityEngine.Events;

namespace Enemies
{
    public class EnemyCore : MonoBehaviour
    {
        private enum EnemyState
        {
            Alive,
            Defeated
        }

        [SerializeField]
        private EnemyMovement enemyMovement;

        [SerializeField]
        private EnemyMovement defeatedMovement;

        [SerializeField]
        private EnemyBulletPattern enemyBulletPattern;

        [SerializeField]
        private Rigidbody2D rigidbody2D;

        [SerializeField]
        private ProtagCore player1;

        [SerializeField]
        private ProtagCore player2;

        public UnityEvent<EnemyCore> OnDefeated;
        public UnityEvent OnRemoveCorpse;

        private EnemyState enemyState;

        private void Update()
        {
            if (enemyMovement == null || enemyBulletPattern == null || player1 == null || player2 == null)
            {
                return;
            }

            if (enemyState == EnemyState.Defeated)
            {
                // If defeated, use defeated movement
                defeatedMovement.CalculateMovement(player1, player2, Time.deltaTime);
            }

            if (enemyState == EnemyState.Alive)
            {
                // Update movement
                enemyMovement.CalculateMovement(player1, player2, Time.deltaTime);

                // Update bullet pattern
                enemyBulletPattern.UpdateBulletPattern(player1, player2, Time.deltaTime);
            }
        }

        public void Initialize(ProtagCore player1, ProtagCore player2)
        {
            this.player1 = player1;
            this.player2 = player2;
            enemyState = EnemyState.Alive;
        }

        public void CleanupCorpse()
        {
            OnRemoveCorpse?.Invoke();
            Destroy(gameObject, 2f);
        }

        public void Defeat(HurtBox.HitData hitData)
        {
            if (enemyState == EnemyState.Defeated)
            {
                return;
            }

            enemyState = EnemyState.Defeated;
            rigidbody2D.linearVelocity = hitData.Direction * hitData.knockbackVel;
            OnDefeated?.Invoke(this);
        }
    }
}