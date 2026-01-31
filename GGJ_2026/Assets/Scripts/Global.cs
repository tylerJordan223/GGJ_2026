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

    //global variables
    public float reputation;
    public int layer;

    private void Start()
    {
        //idea is to go as low as zero and as high as 100
        reputation = 50f;
        layer = 0;
    }
}
