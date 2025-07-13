using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Projectiles
{
    public class ProtagPickup : MonoBehaviour
    {
        [SerializeField]
        private LayerMask pickupMask;

        [SerializeField]
        private UnityEvent onPickup;

        private bool canPickup;

        private bool pickedUp;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!canPickup || pickedUp)
            {
                return;
            }

            if ((pickupMask & (1 << other.gameObject.layer)) != 0)
            {
                var protag = other.GetComponentInParent<ProtagCore>();
                if (protag != null)
                {
                    pickedUp = true;
                    Debug.Log("Picked up! " + protag.name);
                    protag.PickupProjectile();
                    onPickup?.Invoke();

                    Destroy(gameObject);
                }
            }
        }

        public void SetCanPickup(bool enablePickup)
        {
            if (canPickup == enablePickup)
            {
                return;
            }

            StartCoroutine(SetPickup(true, 0.25f));
        }

        private IEnumerator SetPickup(bool value, float delay)
        {
            yield return new WaitForSeconds(delay);
            canPickup = value;
        }
    }
}