using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public int sceneNamber;

    public float changeTime;

    private void Start()
    {
    }

    public void ChangeStart()
    {
        Invoke(nameof(sceneManager), changeTime);
    }

    public void sceneManager()
    {
        switch (sceneNamber)
        {
            case 0:
                break;

            case 1:
                SoundManager.Instance.PlaySE_Sys(0);
                SceneManager.LoadScene(0);
                break;

            case 2:
                SoundManager.Instance.PlaySE_Sys(0);
                SceneManager.LoadScene(1);
                break;

            case 3:
                SoundManager.Instance.PlaySE_Sys(0);
                SceneManager.LoadScene(2);
                break;

            case 4:
                SoundManager.Instance.PlaySE_Sys(0);
                SceneManager.LoadScene(3);
                break;

            case 5:
                SoundManager.Instance.PlaySE_Sys(0);
                SceneManager.LoadScene(4);
                break;

            case 6:
                SoundManager.Instance.PlaySE_Sys(0);
                SceneManager.LoadScene(5);
                break;
        }

    }

}
