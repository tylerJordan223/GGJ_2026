using GameInput;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorScript : MonoBehaviour
{
    private GameObject alert;
    private Global_Input input;

    private void Awake()
    {
        alert = transform.GetChild(0).gameObject;
        alert.SetActive(false);

    }

    private void OnEnable()
    {
        input = new Global_Input();
        input.Player.Next.performed += GoThrough;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            alert.SetActive(true);
            input.Player.Next.Enable();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            alert.SetActive(false);
            input.Player.Next.Disable();
        }
    }

    void GoThrough(InputAction.CallbackContext context)
    {
        alert.SetActive(false);
        input.Player.Next.Disable();
        RoomHandler.Instance.SwapLayers();
    }
}
