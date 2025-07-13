using UnityEngine;

namespace Enemies.Movement
{
    public abstract class EnemyMovement : MonoBehaviour
    {
        [Header("Movement Depends")]
        [SerializeField]
        protected Rigidbody2D rigidbody2D;
        
        public abstract void UpdateMovement(Transform player1, Transform player2, float timeDelta);
    }
}
