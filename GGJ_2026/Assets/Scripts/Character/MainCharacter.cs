using UnityEngine;
using UnityEngine.InputSystem;
using GameInput;

public class MainCharacter : MonoBehaviour
{

    //player  controls
    private Global_Input gi;
    private InputAction movement;
    public float moveSpeed;
    private Rigidbody2D rb;

    private void Awake()
    {
        gi = new Global_Input();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        //activate the player controls
        gi.Player.Enable();
    }

    private void Update()
    {
        Vector2 movement = gi.Player.Move.ReadValue<Vector2>();

        rb.linearVelocity = movement * moveSpeed;
    }
}
