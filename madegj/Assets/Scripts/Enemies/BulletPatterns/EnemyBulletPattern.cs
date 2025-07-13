using UnityEngine;

namespace Enemies.BulletPatterns
{
    public abstract class EnemyBulletPattern : MonoBehaviour
    {
        public abstract void UpdateBulletPattern(ProtagCore player1, ProtagCore player2, float deltaTime);
    }
}