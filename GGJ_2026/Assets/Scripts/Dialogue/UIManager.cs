using GameInput;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    #region singleton

    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static UIManager instance;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this.gameObject);
        }
        instance = this;
    }

    #endregion singleton

    [SerializeField] private GameObject dialogue;
    public bool dialogue_active;

    public void StartDialogue()
    {
        dialogue_active = true;
        dialogue.SetActive(true);
    }

    public void EndDialogue()
    {
        dialogue_active = false;
        dialogue.SetActive(false);
    }

    private Global_Input input;

    private void Start()
    {
        input = new Global_Input();
        input.Player.Jump.performed += Activate;
        input.Player.Jump.Enable();
    }

    void Activate(InputAction.CallbackContext context)
    {
        if(!dialogue_active)
        {
            StartDialogue();
        }
    }
}
