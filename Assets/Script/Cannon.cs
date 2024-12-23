using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject firePos;

    [SerializeField] GameObject seachObjecgt;
    EnemySearch search;

    public GameObject effect;

    Animator animator;

    float timer = 0.0f;
    public float interval = 3.0f;

    void Start()
    {
        search = seachObjecgt.GetComponent<EnemySearch>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        BulletFire();

        if (animator != null)
        {
            Animation();
        }
    }

    void BulletFire()
    {
        if (search.playerIN)
        {
            timer += Time.deltaTime;

            if (timer >= interval)
            {
                SoundManager.Instance.PlaySE_Game(6);

                Instantiate(bullet, firePos.transform.position, firePos.transform.rotation);
                timer = 0.0f;
            }
        }
    }

    void Animation()
    {
        if (timer >= interval - 1)
        {
            animator.SetBool("Fire", true);
            effect.SetActive(true);
        }
        else if(timer < interval - 1)
        {
            animator.SetBool("Fire", false);
            effect.SetActive(false);
        }
    }
}
