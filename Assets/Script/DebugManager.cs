using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class DebugManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SeaneReset();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SoundManager.Instance.PlayBGM(0);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            GameManager.Instance.Damage(10);
        }
    }

    public void SeaneReset()
    {
        //Instance  Singleton����Instance�֐�������Ă��邽�ߎg�p�ł���

        //FadeManager����V�[���̃��[�h���s��
        FadeManager.Instance.LoadSceneIndex(1, 1);

        //SoundManager����SE���Ăяo��
        SoundManager.Instance.PlaySE_Sys(0);
    }

}
