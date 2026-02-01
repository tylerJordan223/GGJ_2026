using System.Collections.Generic;
using UnityEngine;

public class LadyScript : MonoBehaviour
{
    //singletone
    public static LadyScript Instance
    {
        get
        {
            return instance;
        }
    }
    private static LadyScript instance = null;

    private void Awake()
    {
        if(instance)
        {
            DestroyImmediate(this.gameObject);
        }
        instance = this;
    }

    //possible images
    [SerializeField] List<Sprite> bodies;
    [SerializeField] List<Sprite> faces;
    //actual objects
    [SerializeField] SpriteRenderer face;
    [SerializeField] SpriteRenderer body;
    //collider
    [SerializeField] BoxCollider2D dialogueTrigger;

    //positions throughout the 
    [SerializeField] List<Vector3> positions;
    private Vector3 current_position;
    private Vector3 previous_position;

    private bool moving;
    private float elapsed_time;
    private float duration = 3.0f;

    private void Start()
    {
        //set her as happy initially
        face.sprite = faces[0];
        body.sprite = bodies[0];

        //move her in from the right
        current_position = positions[0];
        previous_position = positions[1];

        TransitionPosition();
    }

    private void FixedUpdate()
    {
        if(moving)
        {
            elapsed_time += Time.deltaTime;
            float percentage = elapsed_time / duration;
            transform.localPosition = Vector3.Lerp(previous_position, current_position, percentage);
            if (percentage >= 1)
            {
                moving = false;
                body.sprite = bodies[Global.Instance.act_num];
            }
        }

        if(Global.Instance.reputation > 50f)
        {
            face.sprite = faces[0];
        }else if(Global.Instance.reputation > 20f)
        {
            face.sprite = faces[1];
        }
        else
        {
            face.sprite = faces[2];
        }
    }

    public void TransitionPosition()
    {
        //set up the positions to lerp
        if (Global.Instance.act_num != 0)
        {
            previous_position = current_position;
            current_position = positions[Global.Instance.act_num];
        }
        elapsed_time = 0;
        moving = true;
    }
}
