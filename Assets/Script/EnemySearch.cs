using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{
    /*[HideInInspector]*/ public bool playerIN , attackFLG;

    [SerializeField] GameObject attackSearch;
    EnemyAttack enemyAttack;

    private void Start()
    {
        enemyAttack = attackSearch.GetComponent<EnemyAttack>();
    }

    private void Update()
    {
        if (enemyAttack.attackIN)
        {
            attackFLG= true;
        }
        else
        {
            attackFLG = false;
        }
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
