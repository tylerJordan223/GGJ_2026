using Unity.Cinemachine;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    //script to handle the camera system

    public GameObject room_cam;
    private BoxCollider2D room_col;

    private RoomHandler parent;

    private void Awake()
    {
        parent = transform.parent.GetComponent<RoomHandler>();

        room_cam = transform.GetChild(0).gameObject;
        room_cam.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        parent.room_queue.Add(room_cam);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        parent.room_queue.Remove(room_cam);
    }
}
