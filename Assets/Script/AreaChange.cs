using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChange : MonoBehaviour
{
    public bool playerIN;

    public GameObject[] changeCamera;

    void Update()
    {
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIN = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
         if (other.gameObject.tag == "Player")
         {
             playerIN = false;
         }
    }
}
