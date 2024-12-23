using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGimmick: MonoBehaviour
{
    enum MoveType
    {
        �펞,
        �N��
    }

    [SerializeField] MoveType type;

    [SerializeField] float moveTime;

    [SerializeField] float interval;

    [SerializeField] GameObject[] routePoint;

    [SerializeField] GameObject trigger;

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
        switch (type)
        {
            case MoveType.�펞:
                AlwaysMove();
                break;

            case MoveType.�N��:
                StartMove();
                break;
        }
    }

    void AlwaysMove()
    {
        if (timer <= moveTime)
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

    void StartMove()
    {
        if (trigger == null)
        {
            if (timer <= moveTime)
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
