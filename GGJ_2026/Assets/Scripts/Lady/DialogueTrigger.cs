using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log(MainCharacter.Instance.canControl);
            if(MainCharacter.Instance.canControl)
            {
                MainCharacter.Instance.transform.Find("InteractAlert").gameObject.SetActive(true);
                MainCharacter.Instance.interactable = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (MainCharacter.Instance.canControl)
            {
                MainCharacter.Instance.transform.Find("InteractAlert").gameObject.SetActive(false);
                MainCharacter.Instance.interactable = false;
            }
        }
    }
}
