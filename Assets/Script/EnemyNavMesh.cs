using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyNavMesh : MonoBehaviour
{
    //移動の方法指定
    enum MoveType　　　//enum　変化しない定数を複数個格納できる変数
    {
        なし,
        追跡,
        巡回,
        巡回から追跡,
        逃走,
        巡回から逃走
    }

    [SerializeField] private MoveType type;

    GameObject target;

    NavMeshAgent nav;

    [SerializeField] GameObject[] routePoint;
    int nexrPoint;

    [SerializeField] GameObject seachObjecgt;
    EnemySearch search;

    public GameObject hitObj;

    bool chase;

    public float speedCs;
    public float angularCs;
    public float accelerationCs;

    public float speedPt;
    public float angularPt;
    public float accelerationPt;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player");

        nav = GetComponent<NavMeshAgent>();

        search = seachObjecgt.GetComponent<EnemySearch>();

        hitObj.SetActive(false);
    }

    void Update()
    {
        if (!GameManager.Instance.state_damage)
        {
            switch (type)
            {
                case MoveType.なし:
                    break;

                case MoveType.追跡:
                    ChasePlayer();
                    break;

                case MoveType.巡回:
                    RoutePat();
                    break;

                case MoveType.巡回から追跡:
                    PatChase();
                    break;

                case MoveType.逃走:
                    RunEnemy();
                    break;

                case MoveType.巡回から逃走:
                    RunEnemy();
                    break;
            }
        }
        
    }

    void ChasePlayer()
    {
        if (search.playerIN && !search.attackFLG)
        {
            if (target != null)
            {
                if (!GameManager.Instance.state_damage)
                {
                    hitObj.SetActive(false);
                    animator.SetBool("Attack", false);
                    animator.SetBool("Run",true);
                    nav.SetDestination(target.transform.position);
                }
            }
        }
        else if(search.attackFLG)
        {
            if (target != null)
            {
                if (!GameManager.Instance.state_damage)
                {
                    animator.SetBool("Run", false);
                    animator.SetBool("Attack", true);

                    if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                    {
                        hitObj.SetActive(true);
                    }
                }
            }
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
            nav.SetDestination(transform.position);
        }
    }

    void RoutePat()
    {
        if (nav.pathPending == false && nav.remainingDistance <= 0.1f)
        {
            nav.destination = routePoint[nexrPoint].transform.position;

            nexrPoint = (nexrPoint + 1) % routePoint.Length;
        }
    }

    void PatChase()
    {
        if (search.playerIN)
        {
            ChasePlayer();
            nav.speed = speedCs;
            nav.acceleration = accelerationCs;
            nav.angularSpeed = angularCs;
        }
        else
        {
            RoutePat();
            nav.speed = speedPt;
            nav.acceleration = accelerationPt;
            nav.angularSpeed = angularPt;
        }
    }

    void RunEnemy()
    {
        if(target != null)
        {
            if (search.playerIN)
            {
                Vector3 dir = transform.position - target.transform.position;

                nav.SetDestination(transform.position + dir * 0.3f);
            }
            else
            {
                nav.SetDestination(transform.position);
            }
        }
    }

    void PatRun()
    {

    }
}
