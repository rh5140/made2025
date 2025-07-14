using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vector2 = UnityEngine.Vector2;

public class ProtagCore : MonoBehaviour
{
    public enum PlayerState
    {
        MOVE,
        ROLL,
        AIM,
        SHOOT,
        DEAD
    }

    public int playerID; // 1 or 2

    public float rollCooldown = 1.0f;
    public float rollDuration = 2.0f;
    public float rollMovementMultiplier = 5.0f;

    // Unit vector representing direction player is facing.
    public Vector2 direction = Vector2.up;

    public List<KeyCode> aimKeys = new() { KeyCode.BackQuote, KeyCode.Period };
    public List<KeyCode> rollKeys = new() { KeyCode.Alpha1, KeyCode.Slash };
    public List<KeyCode> leftKeys = new() { KeyCode.A, KeyCode.LeftArrow };
    public List<KeyCode> rightKeys = new() { KeyCode.D, KeyCode.RightArrow };
    public List<KeyCode> upKeys = new() { KeyCode.W, KeyCode.UpArrow };
    public List<KeyCode> downKeys = new() { KeyCode.S, KeyCode.DownArrow };
    public float movementMultiplier = 5.0f;

    public float rollPrevTime;

    [SerializeField]
    private ProtagMovement protagMovement;

    [SerializeField]
    private ProtagShoot protagShoot;

    [SerializeField]
    private Animator protagAnimator;

    public PlayerState playerState;
    public Rigidbody2D playerRigidBody2d;

    public Collider2D playerCollider2d;

    public UnityEvent onPlayerRevive;
    public UnityEvent onPlayerMove;
    public UnityEvent onPlayerRoll;
    public UnityEvent onPlayerDie;
    public UnityEvent onPlayerAim;
    public UnityEvent onPlayerShoot;
    public UnityEvent onPlayerPickup;
    private bool hasProjectile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        hasProjectile = false;
        ChangeState(PlayerState.MOVE);
        rollPrevTime = -1 * rollCooldown; // Allow players to roll immediately?
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateState();
        HandleState();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 50, 50), playerState.ToString());
        GUI.Label(new Rect(0, 50, 50, 50), rollCooldown - (Time.time - rollPrevTime) + " cooldown");
    }

    private void HandleState()
    {
        if (playerState == PlayerState.MOVE)
        {
            protagMovement.HandleMovement();
            return;
        }

        if (playerState == PlayerState.ROLL)
        {
            protagMovement.HandleRoll();
        }
    }

    private void UpdateState()
    {
        if (playerState != PlayerState.DEAD && playerState != PlayerState.ROLL)
        {
            protagMovement.HandleDirection();
        }

        float curTime = Time.time;
        // Recover from Roll
        if (playerState == PlayerState.ROLL && rollPrevTime <= curTime - rollDuration)
        {
            ChangeState(PlayerState.MOVE);
            playerCollider2d.enabled = true;
            return;
        }

        // Move to Roll
        if (playerState == PlayerState.MOVE && rollPrevTime <= curTime - rollCooldown &&
            Input.GetKeyDown(rollKeys[playerID - 1]))
        {
            rollPrevTime = Time.time;
            ChangeState(PlayerState.ROLL);
            playerCollider2d.enabled = false;
            return;
        }

        // Move to Aim
        if (playerState == PlayerState.MOVE && hasProjectile && Input.GetKeyDown(aimKeys[playerID - 1]))
        {
            ChangeState(PlayerState.AIM);
            return;
        }

        // Aim to Move
        if (playerState == PlayerState.AIM && Input.GetKeyUp(aimKeys[playerID - 1]))
        {
            hasProjectile = false;
            protagShoot.HandleShoot();
            ChangeState(PlayerState.MOVE);
            onPlayerShoot?.Invoke();
        }
    }

    public Vector2 GetPosition()
    {
        return playerRigidBody2d.position;
    }

    public void PickupProjectile()
    {
        hasProjectile = true;
        if (playerState == PlayerState.MOVE)
        {
            ChangeState(PlayerState.MOVE);
        }
        onPlayerPickup?.Invoke();
    }

    public void Die()
    {
        if (playerState == PlayerState.DEAD)
        {
            return;
        }

        ChangeState(PlayerState.DEAD);
        transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
    }

    public void Revive()
    {
        if (playerState != PlayerState.DEAD)
        {
            return;
        }

        ChangeState(PlayerState.MOVE);
        onPlayerRevive?.Invoke();
    }

    private void ChangeState(PlayerState newState)
    {
        playerState = newState;
        switch (newState)
        {
            case PlayerState.MOVE:
                if (hasProjectile)
                {
                    protagAnimator.Play("MoveLoaded");
                }
                else
                {
                    protagAnimator.Play("MoveUnloaded");
                }

                onPlayerMove?.Invoke();
                break;
            case PlayerState.ROLL:
                protagAnimator.Play("Roll");
                onPlayerRoll?.Invoke();
                break;
            case PlayerState.AIM:
                protagAnimator.Play("Aiming");
                onPlayerAim?.Invoke();
                break;
            case PlayerState.DEAD:
                protagAnimator.Play("Dead");
                onPlayerDie?.Invoke();
                break;
        }
    }
}