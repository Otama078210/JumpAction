using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10;
    public float deleteTime = 2;

    Rigidbody rigid;
    
    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        rigid.AddForce(transform.forward * bulletSpeed,ForceMode.Impulse);

        Destroy(gameObject, deleteTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(EffectManager.Instance.StageFX[0], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
