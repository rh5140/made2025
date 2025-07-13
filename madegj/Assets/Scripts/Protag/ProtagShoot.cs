using Projectiles;
using UnityEngine;

public class ProtagShoot : MonoBehaviour
{
    [SerializeField]
    private ProtagCore protagCore;
    [SerializeField]
    private Projectile projectilePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
    }

    // Update is called once per frame
    public void HandleShoot()
    {
        Vector2 direction = protagCore.direction;

        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.Initialize(direction);
    }
}
