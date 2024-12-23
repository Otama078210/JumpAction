using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHitCheck : Singleton<PlayerHitCheck>
{
    public int gameLevel;

    public float respawnX;
    public float respawnY;
    public float respawnZ;

    public float damageEn = 10.0f;
    public float damageSO = 10.0f;

    GameObject player;
    public Image image;

    public float timer = 0.0f;

    float alphaPuls = 0.0f;
    float alphaMinus = 0.0f;

    float aMSave = 0.0f;

    public float fadeTime = 1.0f;
    public float intervalTime = 0.5f;
    public float finTime = 3.0f;

    bool fade;
    bool respawn;
    bool stageOut;
    [HideInInspector] public bool gimmickOn;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        alphaMinus = finTime - (fadeTime + intervalTime);
        aMSave = alphaMinus;
    }

    private void FixedUpdate()
    {
        StageOutFade();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            respawn = true;
            fade = true;

            SoundManager.Instance.PlaySE_Game(5);

            switch (gameLevel)
            {
                case 0:
                    break;

                case 1:
                    if (!stageOut)
                    {
                        GameManager.Instance.StageOut(damageSO / 2);
                        stageOut = true;
                    }
                    break;

                case 2:
                    if(!stageOut)
                    {
                        GameManager.Instance.StageOut(damageSO);
                        stageOut = true;
                    }
                    break;

                case 3:
                    if (!stageOut)
                    {
                        GameManager.Instance.StageOut(damageSO * 2);
                        stageOut = true;
                    }
                    break;
            }
        }

        if (other.transform.tag == "Trigger")
        { 
            Destroy(other.gameObject);
        }

        if (other.transform.tag == "HalfWay")
        {
            Vector3 changeRes = other.transform.position;

            SoundManager.Instance.PlaySE_Game(7);

            respawnX = changeRes.x;
            respawnY = changeRes.y;
            respawnZ = changeRes.z;

            Destroy(other.gameObject);
        }

        if (other.transform.tag == "Item")
        {
            GameManager.Instance.ItemGet();

            SoundManager.Instance.PlaySE_Game(1);

            Instantiate(EffectManager.Instance.playerFX[1], transform.position, Quaternion.identity);

            Destroy(other.gameObject);
        }        

        if(other.transform.tag == "Finish" && GameManager.Instance.clearble == true)
        {
            TimelineManager.Instance.PlayTimeline(3);
        }

        if (other.transform.tag == "Enemy" && GameManager.Instance.gameOver == false && !GameManager.Instance.state_damage)
        {
            Vector3 enemyPos = other.transform.position;
            Vector3 playerPos = this.transform.position;

            if(enemyPos.x > playerPos.x)
            {
                PlayerController.Instance.player.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                PlayerController.Instance.player.rotation = Quaternion.Euler(0, 180, 0);
            }

            SoundManager.Instance.PlaySE_Game(0);

            Instantiate(EffectManager.Instance.playerFX[0], transform.position, Quaternion.identity);

            GameManager.Instance.Damage(damageEn);
        }
    }

    void StageOutFade()
    {
        if (fade && GameManager.Instance.mainGame)
        {
            timer += Time.deltaTime;

            if(timer <= fadeTime)
            {
                PlayerController.Instance.stageOut = true;
                alphaPuls += Time.deltaTime;
                //Debug.Log(alphaMinus);

                image.color = new Color(image.color.r, image.color.g, image.color.b, alphaPuls / fadeTime);
            }            
            else if(timer > fadeTime + intervalTime && timer <= finTime)
            {
                alphaMinus -= Time.deltaTime;
                //Debug.Log(alphaMinus);

                image.color = new Color(image.color.r, image.color.g, image.color.b, alphaMinus / (finTime - (fadeTime + intervalTime)));
            }
            else if(timer > finTime)
            {
                alphaPuls = 0.0f;
                alphaMinus = aMSave;
                timer = 0.0f;
                PlayerController.Instance.stageOut = false;
                fade = false;
            }

            if (respawn && timer > fadeTime)
            {
                SoundManager.Instance.PlaySE_Game(4);
                player.transform.position = new Vector3(respawnX, respawnY, respawnZ);
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
                respawn = false;
                stageOut = false;
            }
        }        
    }
}
