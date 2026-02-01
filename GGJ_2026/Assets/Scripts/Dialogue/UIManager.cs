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
    [SerializeField] private GameObject minigame;
    public bool dialogue_active;

    #region activation
    //turn on the dialogue object and animate it
    public void ActivateDialogue()
    {
        dialogue_active = true;
        dialogue.SetActive(true);
        anim.SetBool("active", true);
        StartCoroutine(ActivateDelay());
    }

    //actually start writing the dialogue
    public IEnumerator ActivateDelay()
    {
        yield return new WaitForSeconds(1.8f);
        dialogue.GetComponent<DialogueSystem>().BeginDialogue();
    }
    #endregion activation

    #region deactivation
    //turn off dialogue and move back to normal
    public void DeactivateDialogue()
    {
        anim.SetBool("active", false);
        StartCoroutine(DeactivateDelay());
    }

    private IEnumerator DeactivateDelay()
    {
        yield return new WaitForSeconds(1.5f);
        dialogue.SetActive(false);
        dialogue_active = false;
        MainCharacter.Instance.ActivatePlayer();
    }
    #endregion deactivation

    private Global_Input input;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        input = new Global_Input();
    }

    public void StartMinigame()
    {
        minigame.SetActive(true);
    }
}
