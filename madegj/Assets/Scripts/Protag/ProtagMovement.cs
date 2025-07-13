using System.Collections.Generic;
using UnityEngine;

public class ProtagMovement : MonoBehaviour
{

    private List<KeyCode> leftKeys = new List<KeyCode> { KeyCode.A, KeyCode.LeftArrow };
    private List<KeyCode> rightKeys = new List<KeyCode> { KeyCode.D, KeyCode.RightArrow};
    private List<KeyCode> upKeys = new List<KeyCode> { KeyCode.W, KeyCode.UpArrow};
    private List<KeyCode> downKeys = new List<KeyCode> { KeyCode.S, KeyCode.DownArrow};

    public float movementMultiplier = 5.0f;

    public Rigidbody2D playerRigidBody2D;

    public void HandleMovement(int playerID)
    {
        Vector2 newVelocity = Vector2.zero;
        if (Input.GetKey(leftKeys[playerID - 1]))
            newVelocity += Vector2.left;
        if (Input.GetKey(rightKeys[playerID - 1]))
            newVelocity += Vector2.right;
        if (Input.GetKey(upKeys[playerID - 1]))
            newVelocity += Vector2.up;
        if (Input.GetKey(downKeys[playerID - 1]))
            newVelocity += Vector2.down;

        newVelocity = newVelocity.normalized;

        playerRigidBody2D.linearVelocity = newVelocity * movementMultiplier;
    }
}
