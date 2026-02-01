using GameInput;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerMinigame : MonoBehaviour
{
    [SerializeField] GameObject fakePlayer;
    private GameObject alert;

    private Global_Input input;
    private bool played;

    private void OnEnable()
    {
        played = false;
        input = new Global_Input();
        input.Player.Next.performed += startMinigame;
        alert = transform.Find("Alert").gameObject;
        alert.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            alert.SetActive(true);
            input.Player.Next.Enable();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            alert.SetActive(false);
            input.Player.Next.Disable();
        }
    }

    private void Update()
    {
        //destroy this object after done
        if (played && MainCharacter.Instance.canControl)
        {
            Destroy(this.gameObject);
        }
    }

    //start the minigame
    void startMinigame(InputAction.CallbackContext context)
    {
        alert.SetActive(false);
        input.Player.Next.Disable();
        fakePlayer.SetActive(true);
        UIManager.Instance.StartMinigame();
        played = true;
    }
}
