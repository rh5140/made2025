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

        public override void UpdateMovement(ProtagCore player1, ProtagCore player2, float timeDelta)
        {
            // find nearest player
            ProtagCore targetPlayer = player1;
            if (Vector3.Distance(transform.position, player2.GetPosition()) <
                Vector3.Distance(transform.position, player1.GetPosition()))
            {
                targetPlayer = player2;
            }

            Vector2 desiredVelocity = (targetPlayer.GetPosition() - (Vector2)transform.position).normalized * speed;
            Vector2 currentVelocity = rigidbody2D.linearVelocity;

            Vector2 newVelocity = Vector2.MoveTowards(currentVelocity,
                desiredVelocity,
                acceleration * timeDelta);

            rigidbody2D.linearVelocity = newVelocity;

            if (newVelocity != Vector2.zero)
            {
                float angle = Mathf.Atan2(newVelocity.y, newVelocity.x) * Mathf.Rad2Deg;
                rigidbody2D.rotation = angle;
            }
        }
    }
}