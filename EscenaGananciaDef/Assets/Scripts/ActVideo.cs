using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActVideo : MonoBehaviour
{
    GameObject roomModel;
    private void Awake()
    {
        roomModel = GameObject.FindGameObjectWithTag("Room3DModel");
    }
    //public bool act;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ActivateVideo(act);
        //if (OVRInput.GetDown(OVRInput.Button.Two))
        //{
        //    Debug.Log("CLICKEDAV" +
        //        "");

            
        //}
    }
    public void ActivateVideo(bool act )
    {
        if (act)
        {
            roomModel.SetActive(true);
        }
        else
        {
            roomModel.SetActive(false);
        }
    }
}


