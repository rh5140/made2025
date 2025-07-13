using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProtagMovement : MonoBehaviour
{
    [SerializeField]
    public ProtagCore protagCore;

    public Rigidbody2D playerRigidBody2D;

    public void HandleMovement()
    {
        int playerID = protagCore.playerID;
        Vector2 newVelocity = Vector2.zero;

        if (Input.GetKey(protagCore.leftKeys[playerID - 1]))
            newVelocity += Vector2.left;
        if (Input.GetKey(protagCore.rightKeys[playerID - 1]))
            newVelocity += Vector2.right;
        if (Input.GetKey(protagCore.upKeys[playerID - 1]))
            newVelocity += Vector2.up;
        if (Input.GetKey(protagCore.downKeys[playerID - 1]))
            newVelocity += Vector2.down;

        Vector2 direction = newVelocity.normalized;
        if (direction != Vector2.zero)
            protagCore.prevDirection = direction;

        playerRigidBody2D.linearVelocity = direction * protagCore.movementMultiplier;
    }

    public void HandleRoll() {
        Debug.Log("handling roll...");
        Debug.Log(protagCore.prevDirection);
        playerRigidBody2D.linearVelocity = protagCore.prevDirection * protagCore.rollMovementMultiplier;
    }
}
