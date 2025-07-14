using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using Vector2 = UnityEngine.Vector2;

public class ProtagCore : MonoBehaviour
{

    public enum PlayerState { MOVE, ROLL, AIM, SHOOT, DEAD }
    public int playerID; // 1 or 2

    public float rollCooldown = 1.0f;
    public float rollDuration = 2.0f;
    public float rollMovementMultiplier = 5.0f;

    // Unit vector representing direction player is facing.
    public Vector2 direction = Vector2.up;

    public List<KeyCode> aimKeys = new List<KeyCode> { KeyCode.BackQuote, KeyCode.Period };
    public List<KeyCode> rollKeys = new List<KeyCode> { KeyCode.Alpha1, KeyCode.Slash };
    public List<KeyCode> leftKeys = new List<KeyCode> { KeyCode.A, KeyCode.LeftArrow };
    public List<KeyCode> rightKeys = new List<KeyCode> { KeyCode.D, KeyCode.RightArrow };
    public List<KeyCode> upKeys = new List<KeyCode> { KeyCode.W, KeyCode.UpArrow };
    public List<KeyCode> downKeys = new List<KeyCode> { KeyCode.S, KeyCode.DownArrow };
    public float movementMultiplier = 5.0f;

    private float rollPrevTime;
    private bool hasProjectile;

    [SerializeField]
    private ProtagMovement protagMovement;
    [SerializeField]
    private ProtagShoot protagShoot;

    [SerializeField]
    private Animator protagAnimator;

    public PlayerState playerState;
    public Rigidbody2D playerRigidBody2d;

    public Collider2D playerCollider2d;

    public UnityEvent onRevive;
    public UnityEvent onPlayerMove;
    public UnityEvent onPlayerRoll;
    public UnityEvent onPlayerDie;
    public UnityEvent onPlayerAim;
    public UnityEvent onPlayerShoot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasProjectile = false;
        ChangeState(PlayerState.MOVE);
        rollPrevTime = -1 * rollCooldown; // Allow players to roll immediately?
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        HandleState();

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
            return;
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
        if (playerState == PlayerState.ROLL && (rollPrevTime <= curTime - rollDuration))
        {
            ChangeState(PlayerState.MOVE);
            playerCollider2d.enabled = true;
            return;
        }

        // Move to Roll
        if (playerState == PlayerState.MOVE && (rollPrevTime <= curTime - rollCooldown) && Input.GetKeyDown(rollKeys[playerID - 1]))
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
            return;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 50, 50), playerState.ToString());
        GUI.Label(new Rect(0, 50, 50, 50), (rollCooldown - (Time.time - rollPrevTime)).ToString() + " cooldown");
    }

    public Vector2 GetPosition()
    {
        return playerRigidBody2d.position;
    }

    public void PickupProjectile()
    {
        hasProjectile = true;
        if (playerState == PlayerState.MOVE)
            ChangeState(PlayerState.MOVE);
    }

    public void Die()
    {
        ChangeState(PlayerState.DEAD);
        transform.rotation = UnityEngine.Quaternion.AngleAxis(0, UnityEngine.Vector3.forward);
    }

    public void Revive()
    {
        ChangeState(PlayerState.MOVE);
    }

    private void ChangeState(PlayerState newState) {
        playerState = newState;
        switch (newState)
        {
            case PlayerState.MOVE:
                if (hasProjectile) protagAnimator.Play("MoveLoaded");
                else protagAnimator.Play("MoveUnloaded");
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
