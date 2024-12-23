using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.EventSystems;

public class GameManager : Singleton<GameManager>
{
    public bool mainGame = false;   
    public bool clearble = false;   
    public bool gameClear = false;  
    public bool gameOver = false;

    public bool state_damage = false;  

    GameObject[] itemObject;
    int itemCountCurrent, itemCountMax;

    [Header("HP")]
    public int hpCurrent = 10;
    public int hpMax = 10;

    [Header("Item")]
    public TextMeshProUGUI currentText;
    public TextMeshProUGUI maxText;

    [Header("HPGauge")]
    public Image hpGauge;
    float hpValue;

    [Header("DemoSkip")]
    [SerializeField] GameObject canvasMainGame;
    [SerializeField] GameObject canvasStartDemo;
    [SerializeField] GameObject pd_parent;
    [SerializeField] GameObject mainCamera;

    public GameObject clearFocus;
    public GameObject falseFocus;

    public float timeCount = 60;
    public TextMeshProUGUI timeCountText;

    void Start()
    {
        TimelineManager.Instance.PlayTimeline(0);

        hpCurrent = hpMax;

        hpGauge.fillAmount = 1;

        itemObject = GameObject.FindGameObjectsWithTag("Item");

        itemCountMax = itemObject.Length;

        maxText.text = itemCountMax.ToString("000");
        currentText.text = itemCountCurrent.ToString("000");
    }

    void FixedUpdate()
    {
        hpCurrent = Mathf.Clamp(hpCurrent, 0, hpMax);

        if((timeCount <= 0 || hpCurrent <= 0) && !gameOver)
        {
            GameOver();
        }      

        if(TimelineManager.Instance.director[0].state == PlayState.Playing && Input.GetButtonDown("Skip"))
        {
            DemoSkip();
        }

        TimeCount();
    }

    void TimeCount()
    {
        if (mainGame)
        {
            timeCount -= Time.deltaTime;
            timeCountText.text = timeCount.ToString("00.0");
        }
    }

    public void ItemGet()
    {
        itemCountCurrent++;
        currentText.text = itemCountCurrent.ToString("000");

        if (itemCountCurrent >= itemCountMax)
        {
            clearble = true;
            Debug.Log("全アイテム取得");
        }
    }

    public void Damage(float damage)
    {
        hpValue = (hpCurrent - damage) / hpMax;
        hpCurrent -= (int)damage;        

        hpGauge.fillAmount = hpValue;

        state_damage = true;

        Debug.Log("HP = " + hpCurrent);
    }

    public void StageOut(float damage)
    {
        hpValue = (hpCurrent - damage) / hpMax;
        hpCurrent -= (int)damage;

        hpGauge.fillAmount = hpValue;
    }

    public void GameClear()
    {
        SoundManager.Instance.PlaySE_Sys(1);
        SoundManager.Instance.StopBGM();
        TimelineManager.Instance.PlayTimeline(1);
        mainGame = false;
        gameClear = true;
        Debug.Log("ゲームクリア");
    }

    public void ClearFocus()
    {
        EventSystem.current.SetSelectedGameObject(clearFocus);
    }

    public void GameOver()
    {
        SoundManager.Instance.PlaySE_Sys(2);
        SoundManager.Instance.StopBGM();
        TimelineManager.Instance.PlayTimeline(2);
        mainGame = false;
        gameOver = true;
        Debug.Log("ゲームオーバー");
    }

    public void FalseFocus()
    {
        EventSystem.current.SetSelectedGameObject(falseFocus);
    }

    public void MainGameFLG(bool flg)
    {
        mainGame = flg;
    }

    public void DemoSkip()
    {
        TimelineManager.Instance.StopTimeline(0);

        canvasMainGame.SetActive(true);   
        canvasStartDemo.SetActive(false); 
        pd_parent.SetActive(false);       
        mainCamera.SetActive(true);       

        SoundManager.Instance.PlayBGM(0);
        mainGame = true;
    }


}
