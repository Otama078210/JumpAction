using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    [Header("PlayerEffect")]
    public GameObject[] playerFX;

    [Header("StageEffect")]
    public GameObject[] StageFX;

    [Header("OtherEffect")]
    public GameObject[] otherFX;

    void Start()
    {

    }

}
