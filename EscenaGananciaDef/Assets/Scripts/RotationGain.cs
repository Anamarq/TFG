using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationGain : MonoBehaviour
{
    public GameObject CameraRig;
    public GameObject CentralEye;
    private float rotGain=1;
    private float Yant=0;
    private float YTrack;
    // Start is called before the first frame update
    void Start()
    {

        float YTrack = CentralEye.transform.localRotation.eulerAngles.y;  //rotacion y
    }

    // Update is called once per frame
    void Update()
    {
        
        /*
        float AngleY = CentralEye.transform.localRotation.eulerAngles.y;  //rotacion y
        if (AngleY > 180) { AngleY -= 360; }
        Debug.Log("Real angle y: " + AngleY);
        float angleRot = AngleY * (rotGain-1);
        Debug.Log("Gain: "+angleRot);
       TrackingSpace.transform.rotation=Quaternion.Euler(0, angleRot, 0);
       */
        float AngleY = CentralEye.transform.localRotation.eulerAngles.y;  //rotacion y
        ////This is for the problem of the (0-360) degrees. If there is a change of more than 180º, It's supposed that
        ////we passed from that limit
        float incY = AngleY - Yant; 
        if (incY > 180) { incY -= 360; }
        if (incY < -180) { incY += 360; }
        float angleRot=  incY*rotGain-incY; //Rotation increment of the tracking
        YTrack += angleRot; //Add the increment

        CameraRig.transform.rotation = Quaternion.Euler(0, YTrack, 0); 

        Yant = AngleY;
       // Debug.Log("Gain: " + rotGain);

    }

    public void setGain(float newRotGain)
    {
        rotGain = newRotGain;
    }
}
