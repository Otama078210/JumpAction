using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : Singleton<GameManager>
{
    public bool gameStart = false;  
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

    [Header("Demo")]
    [SerializeField] PlayableDirector pd_gameStart;
    //[SerializeField] PlayableDirector pd_gameClear; ゲームをクリアしたときに演出を再生する（仮）

    [Header("DemoSkip")]
    [SerializeField] GameObject canvasMainGame;
    [SerializeField] GameObject canvasStartDemo;
    [SerializeField] GameObject pd_parent;
    [SerializeField] GameObject mainCamera;

    public GameObject canvasGameClear;
    public GameObject canvasGameOver;

    public float timeCount = 60;
    public TextMeshProUGUI timeCountText;

    void Start()
    {
        mainGame = true;

        pd_gameStart.Play();

        hpCurrent = hpMax;

        hpGauge.fillAmount = 1;

        itemObject = GameObject.FindGameObjectsWithTag("Item");

        itemCountMax = itemObject.Length;

        maxText.text = itemCountMax.ToString("000");
        currentText.text = itemCountCurrent.ToString("000");

        canvasGameClear.SetActive(false);
        canvasGameOver.SetActive(false);
    }

    void FixedUpdate()
    {
        hpCurrent = Mathf.Clamp(hpCurrent, 0, hpMax);

        if(timeCount <= 0 || hpCurrent <= 0 && !gameOver)
        {
            GameOver();
        }      

        if(pd_gameStart.state == PlayState.Playing && Input.GetButtonDown("Skip"))
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

    void GameOver()
    {
        SoundManager.Instance.PlaySE_Sys(2);
        SoundManager.Instance.StopBGM();
        canvasGameOver.SetActive(true);
        mainGame = false;
        gameOver = true;
        Debug.Log("ゲームオーバー");
    }

    public void GameClear()
    {
        SoundManager.Instance.PlaySE_Sys(1);
        SoundManager.Instance.StopBGM();
        canvasGameClear.SetActive(true);
        mainGame = false;
        gameClear = true;
        Debug.Log("ゲームクリア");
    }
    
    public void MainGameFLG(bool flg)
    {
        mainGame = flg;
    }

    public void DemoSkip()
    {
        pd_gameStart.Stop();

        canvasMainGame.SetActive(true);   
        canvasStartDemo.SetActive(false); 
        pd_parent.SetActive(false);       
        mainCamera.SetActive(true);       

        SoundManager.Instance.PlayBGM(0);
        mainGame = true;
    }


}
