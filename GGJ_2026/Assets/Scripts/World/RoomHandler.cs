using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    public static RoomHandler Instance
    {
        get
        {
            return instance;
        }
    }

    private static RoomHandler instance = null;

    public GameObject active_room;
    public List<GameObject> room_queue = new List<GameObject>();

    [SerializeField] SpriteRenderer Background;
    [SerializeField] Sprite front_layer;
    [SerializeField] Sprite back_layer;

    [SerializeField] GameObject hallway;
    [SerializeField] GameObject backHouse;

    private void Awake()
    {
        if(instance)
        {
            DestroyImmediate(this.gameObject);
        }
        instance = this;

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

    //swap between the two layers
    public void SwapLayers()
    {
        StartCoroutine(SwapTransition());
    }

    public IEnumerator SwapTransition()
    {
        MainCharacter.Instance.DeactivatePlayer();
        yield return new WaitForSeconds(1.0f);
        //swap front to back
        if (Background.sprite == front_layer)
        {
            backHouse.SetActive(true);
            if(MainCharacter.Instance.transform.position.x < -30.0)
            {
                backHouse.transform.GetChild(0).GetComponent<RoomScript>().Restart();
            }
            else
            {
                backHouse.transform.GetChild(1).GetComponent<RoomScript>().Restart();
            }
            Background.sprite = back_layer;
            hallway.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            Global.Instance.layer = 1;
        }
        else //swap back to front
        {
            hallway.SetActive(true);
            hallway.GetComponent<RoomScript>().Restart();
            Background.sprite = front_layer;
            backHouse.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            Global.Instance.layer = 0;
        }
        MainCharacter.Instance.ActivatePlayer();
    }
}
