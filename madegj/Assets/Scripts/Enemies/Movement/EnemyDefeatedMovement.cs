using UnityEngine;

namespace Enemies.Movement
{
    public class EnemyDefeatedMovement : EnemyMovement
    {
        [SerializeField]
        private float friction;

        public override void CalculateMovement(ProtagCore player1, ProtagCore player2, float timeDelta)
        {
            Vector2 vel = rigidbody2D.linearVelocity;
            Vector2 newVel = Vector2.MoveTowards(vel, Vector2.zero, friction * timeDelta);
            rigidbody2D.linearVelocity = newVel;
        }
    }
}