using GameInput;
using System.Collections;
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

    public void ActivateDialogue()
    {
        dialogue_active = true;
        dialogue.SetActive(true);
        anim.SetBool("active", true);
    }

    public void DeactivateDialogue()
    {
        anim.SetBool("active", false);
        StartCoroutine(DeactivateDelay());
    }

    private IEnumerator DeactivateDelay()
    {
        yield return new WaitForSeconds(1.0f);
        dialogue.SetActive(false);
        dialogue_active = false;
    }

    //actually start writing the dialogue
    public void StartDialogue()
    {
        dialogue.GetComponent<DialogueSystem>().BeginDialogue();
    }

    private Global_Input input;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        input = new Global_Input();
        input.Player.Next.performed += Activate;
        input.Player.Next.Enable();
    }

    void Activate(InputAction.CallbackContext context)
    {
        if(!dialogue_active)
        {
            ActivateDialogue();
        }
    }
}
