using UnityEngine;
using UnityEngine.Events;

public class HurtBox : MonoBehaviour
{
    public struct HitData
    {
        public Vector2 Position;
        public Vector2 Direction;

        public HitData(Vector2 position, Vector2 direction)
        {
            Position = position;
            Direction = direction;
        }
    }

    public UnityEvent<HitData> OnHit;

    [SerializeField]
    private bool isHitboxEnabled = true;

    public bool IsHitboxEnabled => isHitboxEnabled;

    public void Hit(HitData hitData)
    {
        // Trigger the OnHit event when the hurtbox is hit
        OnHit?.Invoke(hitData);
    }

    public void SetHitboxEnabled(bool enabled)
    {
        isHitboxEnabled = enabled;
    }
}