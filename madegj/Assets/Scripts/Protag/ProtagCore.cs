using UnityEngine;

public class ProtagCore : MonoBehaviour
{

    enum PlayerState { MOVE, ROLL, AIM, SHOOT, DEAD }

    [SerializeField]
    public ProtagMovement protagMovement;

    private PlayerState playerState;
    public Rigidbody2D playerRigidBody2d;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerState = PlayerState.MOVE;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == PlayerState.MOVE) protagMovement.HandleMovement();
    }

    Vector2 GetPosition()
    {
        return playerRigidBody2d.position;
    }
}
