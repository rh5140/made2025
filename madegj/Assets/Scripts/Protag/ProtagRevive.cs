using UnityEngine;
using UnityEngine.Events;

public class ProtagRevive : MonoBehaviour
{
    public UnityEvent onReviveTriggerEnter;

    [SerializeField]
    private ProtagCore protagCore;

    [SerializeField]
    private LayerMask reviveMask;

    [SerializeField]
    private float reviveTime;

    [SerializeField]
    private UnityEvent onReviveStart;

    [SerializeField]
    private UnityEvent onReviveEnd;

    private float reviveTimer;

    private bool reviving;

    private void Update()
    {
        if (reviving)
        {
            reviveTimer -= Time.deltaTime;
            if (reviveTimer <= 0)
            {
                onReviveTriggerEnter?.Invoke();
                onReviveEnd?.Invoke();
                reviving = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!reviving)
        {
            return;
        }

        if ((reviveMask & (1 << other.gameObject.layer)) != 0)
        {
            if (protagCore.playerState == ProtagCore.PlayerState.DEAD)
            {
                reviving = false;
                onReviveEnd?.Invoke();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (reviving)
        {
            return;
        }

        if ((reviveMask & (1 << collision.gameObject.layer)) != 0)
        {
            if (protagCore.playerState == ProtagCore.PlayerState.DEAD)
            {
                reviveTimer = reviveTime;
                reviving = true;
                onReviveStart?.Invoke();
            }
        }
    }
}