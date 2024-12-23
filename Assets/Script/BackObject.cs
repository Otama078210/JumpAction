using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackObject : MonoBehaviour
{
    public GameObject moveCamera;
    public GameObject moveObject;

    Vector3 cameraDefault;
    Vector3 cameraPos;

    Vector3 objectDefault;

    void Start()
    {
        cameraDefault = moveCamera.transform.position;
        objectDefault = moveObject.transform.position;
    }

    void Update()
    {
        CameraPos();
        DistancePlus();
    }

    void CameraPos()
    {
        cameraPos = moveCamera.transform.position;
    }

    void DistancePlus()
    {
        if(cameraPos.y > cameraDefault.y)
        {
            float difference = cameraPos.y - cameraDefault.y;

            Vector3 plusPos = objectDefault;

            plusPos.y = objectDefault.y + difference / 2;

            moveObject.transform.position = plusPos;
        }
    }
}
