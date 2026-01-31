using UnityEngine;
using UnityEngine.InputSystem;
using GameInput;

public class MainCharacter : MonoBehaviour
{
    public static MainCharacter Instance
    {
        get
        {
            return instance;
        }
    }
    private static MainCharacter instance = null;


    //player  controls
    private Global_Input gi;
    private InputAction movement;
    public float moveSpeed;
    private Rigidbody2D rb;

    private void Awake()
    {
        //handling the singleton
        if(instance)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        instance = this;
        gi = new Global_Input();
        rb = GetComponent<Rigidbody2D>();
    }

    public bool canControl;
    public bool interactable;

    private void Start()
    {
        canControl = true;
        interactable = false;
    }

    private void OnEnable()
    {
        //add the player controls
        gi.Player.Next.performed += Interact;

        //enable the player itself
        ActivatePlayer();
    }

    private void Update()
    {
        Vector2 movement = gi.Player.Move.ReadValue<Vector2>();

        rb.linearVelocity = movement * moveSpeed;
    }

    void Interact(InputAction.CallbackContext context)
    {
        //if you can interact
        if(interactable && canControl)
        {
            DeactivatePlayer();
            UIManager.Instance.ActivateDialogue();
            //disable the activate thingy
            transform.Find("InteractAlert").gameObject.SetActive(false);
        }
    }

    #region activate/deactivate player
    public void ActivatePlayer()
    {
        canControl = true;
        gi.Player.Enable();
    }

    public void DeactivatePlayer()
    {
        canControl = false;
        gi.Player.Disable();
    }
    #endregion activate/deactivate player
}
