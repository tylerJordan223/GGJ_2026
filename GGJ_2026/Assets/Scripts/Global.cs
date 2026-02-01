using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Global : MonoBehaviour
{
    //SINGLETON FOR STORAGE OF VARIABLES//

    #region singleton
    //global instance
    public static Global Instance
    {
        get
        {
            return instance;
        }
    }

    //local instance 
    private static Global instance = null;

    //creating the instance initially
    private void Awake()
    {
        if(instance)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        instance = this;
    }
    #endregion singleton

    [SerializeField] public List<ListWrapper> act_objects;

    //global variables
    public float reputation;
    public int layer;
    public float score;
    public int act_num;

    private void Start()
    {
        //idea is to go as low as zero and as high as 100
        reputation = 50f;
        layer = 0;
        act_num = 0;
    }

    //function to move from one act to the next
    public void NextAct()
    {
        //if theres anything left that shouldnt be (or lady)
        if (act_objects[act_num].list.Count > 0)
        {
            foreach(GameObject o in act_objects[0].list)
            {
                o.SetActive(false);
            }
        }

        act_num += 1;

        //enable new objects
        if (act_num < act_objects.Count)
        {
            if (act_objects[act_num].list.Count > 0)
            {
                foreach (GameObject o in act_objects[act_num].list)
                {
                    Debug.Log("TRUE!!!");
                    o.SetActive(true);
                }
            }
        }
    }
}

[System.Serializable]
public class ListWrapper
{
    public List<GameObject> list;
}
