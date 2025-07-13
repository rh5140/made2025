using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rigidbody2D;

        [SerializeField]
        private float initialSpeed;

        [SerializeField]
        private float acceleration;

        [SerializeField]
        private LayerMask damageMask;

        [SerializeField]
        private LayerMask collisionMask;

        [SerializeField]
        private float lifeTime;

        [SerializeField]
        private bool destroyOnImpact;

        [FormerlySerializedAs("OnCollide")]
        public UnityEvent OnImpact;

        public UnityEvent OnDamageTarget;

        private Vector2 direction;
        private float lifeTimer;

        private bool impactOccurred;

        private void FixedUpdate()
        {
            if (rigidbody2D == null)
            {
                return;
            }

            // Accelerate the projectile
            Vector2 currentVelocity = rigidbody2D.linearVelocity;
            Vector2 newVelocity = Vector2.MoveTowards(currentVelocity,
                direction * initialSpeed,
                acceleration * Time.fixedDeltaTime);

            rigidbody2D.linearVelocity = newVelocity;

            lifeTimer -= Time.fixedDeltaTime;
            if (lifeTimer <= 0f)
            {
                Impact();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the collided object is in the collision mask
            if ((damageMask & (1 << other.gameObject.layer)) != 0)
            {
                var hurtBox = other.GetComponent<HurtBox>();
                if (hurtBox != null && hurtBox.enabled)
                {
                    hurtBox.Hit(new HurtBox.HitData(rigidbody2D.position, direction));
                    OnDamageTarget?.Invoke();
                }
            }

            if ((collisionMask & (1 << other.gameObject.layer)) != 0)
            {
                Impact();
            }
        }

        private void Impact()
        {
            if (impactOccurred)
            {
                return;
            }

            impactOccurred = true;

            OnImpact?.Invoke();
            if (destroyOnImpact)
            {
                Destroy(gameObject, 2f);
            }
        }

        public void Initialize(Vector2 direction)
        {
            this.direction = direction.normalized;
            rigidbody2D.linearVelocity = this.direction * initialSpeed;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            lifeTimer = lifeTime;
            impactOccurred = false;
        }
    }
}