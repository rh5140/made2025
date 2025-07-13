using UnityEngine;

namespace Enemies.Movement
{
    public abstract class EnemyMovement : MonoBehaviour
    {
        [Header("Movement Depends")]

        [SerializeField]
        protected Rigidbody2D rigidbody2D;

        public abstract void CalculateMovement(ProtagCore player1, ProtagCore player2, float timeDelta);
    }
}