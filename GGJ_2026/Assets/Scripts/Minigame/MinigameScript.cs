using GameInput;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MinigameScript : MonoBehaviour
{
    [SerializeField] GameObject Marker;
    [SerializeField] GameObject HitMarker;
    [SerializeField] GameObject Timer;

    public RectTransform MarkerRect;
    public RectTransform HitMarkerRect;
    public RectTransform TimerRect;

    [Range(1.0f, 10.0f)]
    public float width = 5.0f;
    [Range(0.01f, 1.0f)]
    public float speed = 0.5f;
    [Range(0.1f, 1f)]
    public float time = 0.1f;
    private float max_time;

    private int direction;

    private Global_Input input;

    private void OnEnable()
    {
        MainCharacter.Instance.DeactivatePlayer();

        direction = 1;
        MarkerRect = Marker.transform.GetComponent<RectTransform>();

        //create input
        input = new Global_Input();
        input.Player.Jump.performed += Click;
        input.Player.Jump.Enable();

        //create a hitmarker
        HitMarkerRect = Instantiate(HitMarker, transform).GetComponent<RectTransform>();
        HitMarkerRect.localPosition = new Vector3(Random.Range(-width * 100f, width * 100f), MarkerRect.localPosition.y, 0.0f);

        //base time information
        TimerRect = Timer.GetComponent<RectTransform>();
        max_time = TimerRect.localScale.x;
    }

    private void FixedUpdate()
    {
        MarkerRect.localPosition = new Vector3(MarkerRect.localPosition.x + (direction * 10f) * speed, MarkerRect.localPosition.y, MarkerRect.localPosition.z);

        if (MarkerRect.localPosition.x > width * 100f)
        {
            direction = -1;
        }
        if (MarkerRect.localPosition.x < -width * 100f)
        {
            direction = 1;
        }

        //handle the timer
        TimerRect.localScale = new Vector3(TimerRect.localScale.x - (time * Time.deltaTime), TimerRect.localScale.y, TimerRect.localScale.z);

        //fail
        if(TimerRect.localScale.x < 0)
        {
            Global.Instance.score = 1000;
            End(100f);
        }
    }

    void Click(InputAction.CallbackContext context)
    {
        End(Mathf.Abs(Mathf.Abs(MarkerRect.localPosition.x) - Mathf.Abs(HitMarkerRect.localPosition.x)));
    }

    public void End(float s)
    {
        input.Player.Jump.Disable();
        Global.Instance.score = s;
        Destroy(HitMarkerRect.gameObject);
        TimerRect.localScale = new Vector3(max_time, TimerRect.localScale.y, TimerRect.localScale.z);
        this.gameObject.SetActive(false);
        MainCharacter.Instance.ActivatePlayer();
    }
}
