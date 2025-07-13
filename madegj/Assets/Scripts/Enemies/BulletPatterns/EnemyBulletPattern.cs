using UnityEngine;

namespace Enemies.BulletPatterns
{
    public abstract class EnemyBulletPattern : MonoBehaviour
    {
        public abstract void UpdateBulletPattern(Transform player1, Transform player2, float deltaTime);
    }
}
