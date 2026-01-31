using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    public GameObject active_room;
    public List<GameObject> room_queue = new List<GameObject>();

    private void Awake()
    {
        room_queue.Clear();
    }

    private void Update()
    {
        if(room_queue.Count > 0)
        {
            //for the first camera in the scene
            if(active_room == null)
            {
                active_room = room_queue[0];
                active_room.SetActive(true);
            }

            //if its a new room, set it up properly
            if(active_room != room_queue[0])
            {
                //cycle the camera
                active_room.SetActive(false);
                active_room = room_queue[0];
                active_room.SetActive(true);
            }
        }
    }
}
