using UnityEngine;

namespace Projectiles
{
    public class ProtagPickup : MonoBehaviour
    {
        [SerializeField]
        private LayerMask pickupMask;

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the collided object is in the pickup mask
            if ((pickupMask & (1 << other.gameObject.layer)) != 0)
            {
                // Check if the object has a Protag component
                var protag = other.GetComponentInParent<ProtagCore>();
                if (protag != null)
                {
                    Debug.Log("Picked up! " + protag.name);
                    // Call the pickup method on the Protag component
                    protag.PickupProjectile();

                    // Destroy this pickup object
                    Destroy(gameObject);
                }
            }
        }
    }
}