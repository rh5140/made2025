using UnityEngine;

namespace Projectiles
{
    public class ProtagPickup : MonoBehaviour
    {
        [SerializeField]
        private LayerMask pickupMask;

        private bool canPickup;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!canPickup)
            {
                return;
            }

            if ((pickupMask & (1 << other.gameObject.layer)) != 0)
            {
                var protag = other.GetComponentInParent<ProtagCore>();
                if (protag != null)
                {
                    Debug.Log("Picked up! " + protag.name);
                    protag.PickupProjectile();

                    Destroy(gameObject);
                }
            }
        }

        public void SetCanPickup(bool enablePickup)
        {
            canPickup = enablePickup;
        }
    }
}