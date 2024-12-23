using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceMove : MonoBehaviour
{
    public float max = -3.5f;
    public float min = -3.2f;

    float timer = 0.0f;
    float onceTime = 2.0f;

    bool up = true, down = false;

    void Update()
    {
        if(timer <= onceTime)
        {
            this.gameObject.transform.position += new Vector3(0, (max / onceTime) * Time.deltaTime, 0);
        }
    }
}
