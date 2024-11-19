using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGimmick: MonoBehaviour
{
    [SerializeField] float moveTime;

    [SerializeField] float interval;

    [SerializeField] GameObject[] routePoint;

    Rigidbody rig;

    int current = 0;

    int next = 1;

    Vector3 currentPos;

    Vector3 nextPos;


    float timer;

    Vector3 vel;

    void Start()
    {
        rig = GetComponent<Rigidbody>();

        transform.position = routePoint[0].transform.position;
    }

    void FixedUpdate()
    {
        if(timer <= moveTime)
        {
            timer += Time.fixedDeltaTime;

            currentPos = routePoint[current].transform.position;

            nextPos = routePoint[next].transform.position;

            rig.MovePosition(Vector3.Lerp(currentPos, nextPos, timer / moveTime));
        }
        else
        {
            Pause();
        }

        vel = rig.GetPointVelocity(Vector3.zero);
    }


    void Pause()
    {
        if(timer <= moveTime + interval)
        {
            timer += Time.fixedDeltaTime;
        }
        else
        {
            current = next;
            next = (next + 1) % routePoint.Length;

            timer = 0;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.position += vel * Time.fixedDeltaTime;
        }
    }
}
