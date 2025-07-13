using System.Collections.Generic;
using UnityEngine;

public class ProtagCore : MonoBehaviour
{

    enum PlayerState { MOVE, ROLL, AIM, SHOOT, DEAD }
    public int playerID; // 1 or 2

    public float rollCooldown = 1.0f;
    public float rollDuration = 2.0f;

    private List<KeyCode> aimKeys = new List<KeyCode> { KeyCode.BackQuote, KeyCode.Period };
    private List<KeyCode> rollKeys = new List<KeyCode> { KeyCode.Alpha1, KeyCode.Slash };


    private float rollPrevTime;
    private bool hasProjectile;

    [SerializeField]
    public ProtagMovement protagMovement;
    private PlayerState playerState;
    public Rigidbody2D playerRigidBody2d;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerState = PlayerState.MOVE;
        hasProjectile = true;
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
        if (playerState == PlayerState.MOVE) protagMovement.HandleMovement(playerID);
    }

    private void UpdateState()
    {
        // Recover from Roll
        if (playerState == PlayerState.ROLL && (rollPrevTime <= Time.time - rollDuration))
        {
            playerState = PlayerState.MOVE;
            return;
        }

        // Move to Roll
        if (playerState == PlayerState.MOVE && (rollPrevTime <= Time.time - rollCooldown) && Input.GetKeyDown(rollKeys[playerID - 1]))
        {
            rollPrevTime = Time.time;
            playerState = PlayerState.ROLL;
            return;
        }

        // Move to Aim
        if (playerState == PlayerState.MOVE && Input.GetKeyDown(aimKeys[playerID - 1]))
        {
            playerState = PlayerState.AIM;
            return;
        }

        // Aim to Move
        if (playerState == PlayerState.AIM && Input.GetKeyUp(aimKeys[playerID - 1]))
        {
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
}
