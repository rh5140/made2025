using UnityEngine;
using UnityEngine.Events;

namespace Utility
{
    public class CollisionHook : MonoBehaviour
    {
        public UnityEvent<Collider2D> OnTriggerEnter2DEvent;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerEnter2DEvent.Invoke(other);
        }
    }
}