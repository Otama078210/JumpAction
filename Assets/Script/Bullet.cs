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

        Destroy(this.gameObject, deleteTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.name == "Player")
        {
            //Instantiate(EffectManager.Instance.StageFX[0], transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
