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

        public override void UpdateMovement(Transform player1, Transform player2, float timeDelta)
        {
            // find nearest player
            Transform targetPlayer = player1;
            if (Vector3.Distance(transform.position, player2.position) <
                Vector3.Distance(transform.position, player1.position))
            {
                targetPlayer = player2;
            }

            Vector3 desiredVelocity = (targetPlayer.position - transform.position).normalized * speed;
            Vector2 currentVelocity = rigidbody2D.linearVelocity;

            Vector2 newVelocity = Vector2.MoveTowards(currentVelocity,
                desiredVelocity,
                acceleration * timeDelta);

            rigidbody2D.linearVelocity = newVelocity;
        }
    }
}