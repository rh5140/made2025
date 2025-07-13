using Projectiles;
using UnityEngine;

namespace Protag
{
    public class SpawnProjectileAtStart : MonoBehaviour
    {
        [SerializeField]
        private Projectile projectilePrefab;

        [SerializeField]
        private Vector2 spawnDirection;

        private void Start()
        {
            Projectile projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectileInstance.Initialize(spawnDirection);
        }
    }
}