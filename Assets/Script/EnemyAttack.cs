using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [HideInInspector] public bool attackIN;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            attackIN = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            attackIN = false;
        }
    }
}
