using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGimmck : MonoBehaviour
{
    public GameObject firePosition;
    public GameObject bullet;
    public int fireRotation;

    float timer;

    [SerializeField] GameObject seachObjecgt;
    EnemySearch search;

    [SerializeField] private float interval;

    private void Start()
    {
        search = seachObjecgt.GetComponent<EnemySearch>();
    }

    void FixedUpdate()
    {
        if (search.playerIN)
        {
            timer += Time.fixedDeltaTime;

            if (timer >= interval)
            {
                Instantiate(bullet, firePosition.transform.position, Quaternion.Euler(0, fireRotation, 0));

                timer = 0;
            }
        }
    }
}
