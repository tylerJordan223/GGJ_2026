using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacter : MonoBehaviour
{

    //player  controls
    public InputAction controls;
    public float moveSpeed;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        //activate the player controls
        controls.Enable();
    }

    private void Update()
    {
        Vector2 movement = controls.ReadValue<Vector2>();

        rb.linearVelocity = movement * moveSpeed;
    }
}
