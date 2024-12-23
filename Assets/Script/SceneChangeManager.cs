using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneChangeManager : MonoBehaviour
{
    public float interval;

    public void SceneChanger(int sceneNumber)
    {
        SoundManager.Instance.PlaySE_Sys(3);
        FadeManager.Instance.LoadSceneIndex(sceneNumber, interval);
    }
}
