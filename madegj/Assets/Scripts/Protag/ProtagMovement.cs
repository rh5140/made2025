using UnityEngine;

public class ProtagMovement : MonoBehaviour
{

    public float movementMultiplier = 5.0f;

    public Rigidbody2D playerRigidBody2D;

    public void HandleMovement()
    {
        Vector2 newVelocity = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
            newVelocity += Vector2.left;
        if (Input.GetKey(KeyCode.D))
            newVelocity += Vector2.right;
        if (Input.GetKey(KeyCode.W))
            newVelocity += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            newVelocity += Vector2.down;

        newVelocity = newVelocity.normalized;

        playerRigidBody2D.linearVelocity = newVelocity * movementMultiplier;
    }
}
