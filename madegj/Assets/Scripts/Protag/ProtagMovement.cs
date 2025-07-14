using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class ProtagMovement : MonoBehaviour
{
    [SerializeField]
    public ProtagCore protagCore;

    public Rigidbody2D playerRigidBody2D;
    public Vector2 HandleDirection()
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
        {
            protagCore.direction = direction;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            protagCore.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        return direction;
    }

    public void HandleMovement()
    {
        playerRigidBody2D.linearVelocity = HandleDirection() * protagCore.movementMultiplier;
    }

    public void HandleRoll() {
        playerRigidBody2D.linearVelocity = protagCore.direction * protagCore.rollMovementMultiplier;
    }
}
