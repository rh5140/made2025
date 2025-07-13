using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
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

    public PlayerState playerState;
    public Rigidbody2D playerRigidBody2d;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerState = PlayerState.MOVE;
        hasProjectile = false;
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
        if (playerState == PlayerState.MOVE) {
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
        protagMovement.HandleDirection();
        float curTime = Time.time;
        // Recover from Roll
        if (playerState == PlayerState.ROLL && (rollPrevTime <= curTime - rollDuration))
        {
            playerState = PlayerState.MOVE;
            return;
        }

        // Move to Roll
        if (playerState == PlayerState.MOVE && (rollPrevTime <= curTime - rollCooldown) && Input.GetKeyDown(rollKeys[playerID - 1]))
        {
            rollPrevTime = Time.time;
            playerState = PlayerState.ROLL;
            return;
        }

        // Move to Aim
        if (playerState == PlayerState.MOVE && hasProjectile && Input.GetKeyDown(aimKeys[playerID - 1]))
        {
            playerState = PlayerState.AIM;
            return;
        }

        // Aim to Move
        if (playerState == PlayerState.AIM && Input.GetKeyUp(aimKeys[playerID - 1]))
        {
            // hasProjectile = false;
            protagShoot.HandleShoot();
            playerState = PlayerState.MOVE;
            
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
    }

    public void Die()
    {
        playerState = PlayerState.DEAD;
    }
}
