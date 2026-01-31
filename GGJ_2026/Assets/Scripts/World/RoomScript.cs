using Unity.Cinemachine;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    //script to handle the camera system

    public GameObject room_cam;
    private BoxCollider2D room_col;
    private SpriteRenderer cover;

    public RoomHandler parent;

    public bool active;
    public float fade;

    private void Awake()
    {
        parent = transform.parent.parent.GetComponent<RoomHandler>();

        room_cam = transform.GetChild(0).gameObject;
        room_cam.SetActive(false);

        cover = transform.GetChild(1).GetComponent<SpriteRenderer>();

        active = false;
        fade = 1.0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        parent.room_queue.Add(room_cam);

        active = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        parent.room_queue.Remove(room_cam);

        active = false;
    }

    private void Update()
    {
        if(active && fade > 0)
        {
            fade -= 0.01f;
            cover.color = new Color(cover.color.r, cover.color.g, cover.color.b, fade);

            if(fade < 0)
            {
                fade = 0;
            }
        }

        if(!active && fade < 1.0f)
        {
            fade += 0.01f;
            cover.color = new Color(cover.color.r, cover.color.g, cover.color.b, fade);

            if(fade > 1.0f)
            {
                fade = 1.0f;
            }
        }
    }

    public void Restart()
    {
        fade = 1.0f;
        active = true;
    }
}
