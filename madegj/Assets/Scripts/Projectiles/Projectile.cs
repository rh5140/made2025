using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        private enum ProjectileState
        {
            Flying,
            Impacted
        }

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

        [SerializeField]
        private float knockbackForce;

        [SerializeField]
        private bool stickToTarget;

        [FormerlySerializedAs("OnCollide")]
        public UnityEvent OnImpact;

        public UnityEvent OnDamageTarget;

        private Vector2 direction;
        private float lifeTimer;

        private ProjectileState state = ProjectileState.Flying;

        private Vector2 relativePosition;
        private Transform impaledTarget;

        private void FixedUpdate()
        {
            if (rigidbody2D == null)
            {
                return;
            }

            if (state == ProjectileState.Flying)
            {
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
            else
            {
                if (impaledTarget)
                {
                    rigidbody2D.position = (Vector2)impaledTarget.position + relativePosition;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the collided object is in the collision mask
            if ((damageMask & (1 << other.gameObject.layer)) != 0)
            {
                if (state != ProjectileState.Flying)
                {
                    return;
                }

                var hurtBox = other.GetComponent<HurtBox>();
                if (hurtBox != null && hurtBox.IsHitboxEnabled)
                {
                    hurtBox.Hit(new HurtBox.HitData(rigidbody2D.position, direction, knockbackForce));
                    OnDamageTarget?.Invoke();

                    if (stickToTarget)
                    {
                        relativePosition = (Vector2)other.transform.position - rigidbody2D.position;
                        impaledTarget = other.transform;
                    }
                }
            }

            if ((collisionMask & (1 << other.gameObject.layer)) != 0)
            {
                Impact();
            }
        }

        private void Impact()
        {
            if (state == ProjectileState.Impacted)
            {
                return;
            }

            state = ProjectileState.Impacted;

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
            state = ProjectileState.Flying;
        }
    }
}