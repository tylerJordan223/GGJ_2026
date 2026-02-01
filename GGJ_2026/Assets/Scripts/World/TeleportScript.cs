using GameInput;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportScript : MonoBehaviour
{
    //teleports the players between two points in the world

    [SerializeField] Transform Point1;
    [SerializeField] Transform Point2;
    private GameObject InteractAlert;

    //discern between two points
    private Transform active_point;
    private Transform teleport_point;

    //input
    private Global_Input input;

    //animation
    private Animator anim;

    //random flag
    public bool stairs;

    private void Awake()
    {
        InteractAlert = transform.GetChild(0).gameObject;
        anim = GetComponent<Animator>();
        anim.Play("teleportDownStair", 0, 1.0f);
    }

    private void OnEnable()
    {
        //initalize new input
        input = new Global_Input();

        input.Player.Next.performed += StartTeleport;
        input.Player.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //get the closer teleporter and work based on that
        if(collision.CompareTag("Player") && Global.Instance.layer == 0)
        {
            input.Player.Enable();

            active_point = null;

            if( (Point1.position - MainCharacter.Instance.transform.position).magnitude > (Point2.position - MainCharacter.Instance.transform.position).magnitude )
            {
                active_point = Point2;
                teleport_point = Point1;
            }
            else
            {
                active_point = Point1;
                teleport_point = Point2;
            }

            //enable the notification point on the closer object
            active_point.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && Global.Instance.layer == 0)
        {
            //disable the notification if leaving
            active_point.GetChild(0).gameObject.SetActive(false);
            input.Player.Disable();
        }
    }

    void StartTeleport(InputAction.CallbackContext context)
    {
        input.Player.Disable();
        //disable player, pause, move players
        MainCharacter.Instance.DeactivatePlayer(true);
        active_point.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(Teleport());
    }
    private IEnumerator Teleport()
    {
        if(!stairs)
        {
            //animate
            anim.SetBool("active", true);
            //wait a second for the animation
            yield return new WaitForSeconds(3.0f);
            //teleport
            MainCharacter.Instance.transform.position = teleport_point.position;
            //wait a second
            anim.SetBool("active", false);
            yield return new WaitForSeconds(5.0f);
            //enable player
            MainCharacter.Instance.ActivatePlayer();
        }
        else
        {
            if(anim.GetBool("up"))
            {
                anim.SetBool("up", false);
                //animate
                anim.SetBool("down", true);
                //wait a second for the animation
                yield return new WaitForSeconds(1.0f);
                //teleport
                MainCharacter.Instance.transform.position = teleport_point.position;
                //wait a second
                yield return new WaitForSeconds(1.0f);
                //enable player
                MainCharacter.Instance.ActivatePlayer();
            }
            else
            {
                anim.SetBool("down", false);
                //animate
                anim.SetBool("up", true);
                //wait a second for the animation
                yield return new WaitForSeconds(3.0f);
                //teleport
                MainCharacter.Instance.transform.position = teleport_point.position;
                //wait a second
                yield return new WaitForSeconds(1.0f);
                //enable player
                MainCharacter.Instance.ActivatePlayer();
            }
        }
    }
}
