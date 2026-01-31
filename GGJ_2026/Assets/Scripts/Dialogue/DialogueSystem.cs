using GameInput;
using Ink.Runtime;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    //Actual story being printed out
    public static event Action<Story> OnCreateStory;
    public Story story;

    //Text input
    [SerializeField] public TextAsset inkJSON = null;

    //prefabs
    [SerializeField] GameObject choice = null;

    //Canvas information
    [SerializeField] VerticalLayoutGroup choices = null;
    [SerializeField] TextMeshProUGUI dialogue_box;

    //Input system
    private Global_Input input;
    private float enter;

    //other variables
    private bool printing;
    private bool skip;
    private bool choosing;
    private int choice_num;

    private void Awake()
    {
        input = new Global_Input();
        printing = false;
        skip = false;
        choosing = false;
        choice_num = -1;

        this.gameObject.SetActive(false);
    }

    //enables the controls for dialogue
    private void OnEnable()
    {
        //add the controls
        input.Dialogue.Choose.performed += Click;
        input.Dialogue.Choice.performed += Choose;
        input.Dialogue.Choose.Enable();

        //clear the choices and start the story
        ClearChoices();

        //create new story object using input json
        story = new Story(inkJSON.text);
        //make sure it refreshes itself for debug purposes
        if (OnCreateStory != null) OnCreateStory(story);

        //connect to function within code to affect global reputation
        story.BindExternalFunction("ChangeReputation", (float amount) =>
        {
            Global.Instance.reputation += amount;
        });

        DialogueLoop();
    }

    //diables the controls for dialogue
    private void OnDisable()
    {
        input.Dialogue.Choose.performed -= Click;
        input.Dialogue.Choice.performed -= Choose;
        input.Dialogue.Choose.Disable();
    }

    //coroutine to actually run dialogue
    private void DialogueLoop()
    {
        Debug.Log("Reputation is: " + Global.Instance.reputation);

        //goes until it reaches a stopping point (choice/end)
        if(story.canContinue)
        {
            string text = story.Continue();
            text = text.Trim();
            StartCoroutine(PrintDialogue(dialogue_box, text));
        }
        else if(story.currentChoices.Count > 0) //implies choices
        {
            //you are now making a choice
            choosing = true;

            //initialize choices
            StartCoroutine(InitializeChoices());
        }
    }

    //coroutine to acutally print the dialogue out
    private IEnumerator PrintDialogue(TextMeshProUGUI text, string dialogue)
    {
        printing = true;

        //clear the text before typing it
        text.text = "";

        float time_between = 0.1f;
        //do math if its choices to make sure they all print out at the same time to not mess with dialogue
        if(story.currentChoices.Count > 0)
        {
            //make them all take 1.5 seconds to write out
            time_between = 1.5f / dialogue.Length;
        }

        //loop for typing the text
        for(int i = 0; i < dialogue.Length; i++)
        {
            text.text = text.text + dialogue[i];
            yield return new WaitForSeconds(time_between);

            //if you skip just set the dialogue to max
            if(skip)
            {
                text.text = dialogue;
                break;
            }
        }

        //quick pause
        yield return new WaitForSeconds(0.1f);

        if(skip)
        {
            skip = false;
        }

        printing = false;
    }

    //loop to initialize choices
    private IEnumerator InitializeChoices()
    {
        //go through each choice and initialize them
        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            GameObject c = Instantiate(choice);
            c.transform.SetParent(choices.transform, false);

            //scale properly
            RectTransform rt = c.GetComponent<RectTransform>();
            rt.localScale = Vector3.one;
            rt.localPosition = Vector3.one;
        }

        //wait a frame to scale right
        yield return new WaitForEndOfFrame();

        //rebuild the canvas first so the choices don't scale weird
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(choices.GetComponent<RectTransform>());

        //have them start typing out
        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            TextMeshProUGUI c = choices.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            StartCoroutine(PrintDialogue(c.GetComponent<TextMeshProUGUI>(), story.currentChoices[i].text.Trim()));
        }

        //move to making the choices
        StartCoroutine(making_choice());
    }

    //loop for the choices
    private IEnumerator making_choice()
    {
        //wait at first
        while(printing)
        {
            yield return new WaitForSeconds(0.1f);
        }

        //enable the controls for choosing
        input.Dialogue.Choice.Enable();

        //select the first choice
        choice_num = 0;
        TextMeshProUGUI choice_text = choices.transform.GetChild(choice_num).GetComponent<TextMeshProUGUI>();
        choice_text.text = "-" + choice_text.text + "-";

        while (choosing)
        {
            if(choices.transform.GetChild(choice_num).GetComponent<TextMeshProUGUI>() != choice_text)
            {
                choice_text.text = choice_text.text.Substring(1, choice_text.text.Length-2);
                choice_text = choices.transform.GetChild(choice_num).GetComponent<TextMeshProUGUI>();
                choice_text.text = "-" + choice_text.text + "-";
            }
            yield return new WaitForSeconds(0.01f);
        }
        
        //diable the controls for choosing
        input.Dialogue.Choice.Disable();
    }

    //function to destroy all choices before making new ones
    private void ClearChoices()
    {
        foreach(Transform child in choices.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void Click(InputAction.CallbackContext context)
    {
        if (printing)
        {
            skip = true;
        }
        else
        {
            Debug.Log(story.canContinue + " " + choosing + " " + story.currentChoices.Count);

            //if theres still content to go through
            if (story.canContinue || !choosing)
            {
                //check to end dialogue if necessary
                if (!story.canContinue && story.currentChoices.Count == 0)
                {
                    //end dialogue
                    UIManager.Instance.EndDialogue();
                }
                DialogueLoop();
            }
            else if (story.currentChoices.Count > 0)
            {
                //pick option
                story.ChooseChoiceIndex(choice_num);
                choosing = false;
                ClearChoices();
                DialogueLoop();
            }
        }
    }

    //swapping between choices when allowed
    void Choose(InputAction.CallbackContext context)
    {
        if(choosing)
        {
            //getting input
            Vector2 menu = input.Dialogue.Choice.ReadValue<Vector2>();
            //move through the list
            //up
            if(menu.y > 0.0f)
            {
                if (choice_num == 0)
                {
                    choice_num = story.currentChoices.Count - 1;
                }
                else
                {
                    choice_num -= 1;
                }
            }
            //down
            else if(menu.y < 0.0f)
            {
                if (choice_num == story.currentChoices.Count - 1)
                {
                    choice_num = 0;
                }
                else
                {
                    choice_num += 1;
                }
            }
        }
    }
}
