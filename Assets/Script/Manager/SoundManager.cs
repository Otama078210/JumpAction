using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using SaveData_Settings;

public class SoundManager : Singleton<SoundManager>
{
    [Header("ControlMixer")]
    [SerializeField] public AudioMixer mixer;
    [SerializeField, Label("MasterGroup")] public AudioMixerGroup MasterGroup;
    [SerializeField, Label("BGMGroup")] public AudioMixerGroup BGMGroup;
    [SerializeField, Label("SEGroup")] public AudioMixerGroup SEGroup;
    [Space(10)]

    public AudioClip[] bgmClip;
    public AudioClip[] se_SysClip;
    public AudioClip[] se_GameClip;

    [System.NonSerialized] public AudioSource BGMSource;
    [System.NonSerialized] public AudioSource SE_SysSource;
    [System.NonSerialized] public AudioSource[] SE_GameSource;

    float[] vol_BGM = {-80f, -30f, -27, -24f, -21f, -18f, -15f, -12.5f, -10f, -7.5f, -5f };
    float[] vol_SE  = {-80f, -30f, -27, -24f, -21f, -18f, -15f, -12.5f, -10f, -7.5f, -5f };

    float bgmVol;

    void Awake()
    {
        Load.Audio();

        BGMSource = gameObject.AddComponent<AudioSource>();
        BGMSource.loop = true;
        BGMSource.priority = 0;
        BGMSource.outputAudioMixerGroup = BGMGroup;

        SE_SysSource = gameObject.AddComponent<AudioSource>();
        SE_SysSource.loop = false;
        SE_SysSource.priority = 1;
        SE_SysSource.outputAudioMixerGroup = SEGroup;

        SE_GameSource = new AudioSource[se_GameClip.Length];

        for (int i = 0; i < se_GameClip.Length; i++)
        {
            if (se_GameClip[i] != null)
            {
                SE_GameSource[i] = gameObject.AddComponent<AudioSource>();
                SE_GameSource[i].loop = false;
                SE_GameSource[i].priority = 1;
                SE_GameSource[i].clip = se_GameClip[i];
                SE_GameSource[i].outputAudioMixerGroup = SEGroup;
            }
        }
    }

    public void PlayBGM(int i)
    {
        BGMSource.clip = bgmClip[i];
        BGMSource.Play();
    }

    public void StopBGM()
    {
        BGMSource.Stop();
    }

    public void PlaySE_Sys(int i)
    {
        SE_SysSource.clip = se_SysClip[i];
        SE_SysSource.Play();
    }

    public void PlaySE_Game(int i)
    {
        SE_GameSource[i].Play();
    }

    public void VolumeChange(int vol1,int vol2)
    {
        mixer.SetFloat("BGVol", vol_BGM[vol1]);
        mixer.SetFloat("SEVol", vol_SE[vol2]);

        bgmVol = vol_BGM[vol1];
    }

    public IEnumerator FadeOut(float interval)
    {
        float time = 0;
        while (time <= interval)
        {
            float bgm = Mathf.Lerp(bgmVol, -80f, time / interval);
            mixer.SetFloat("BGVol", bgm);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator FadeIn(float interval)
    {
        float time = 0;
        while (time <= interval)
        {
            float bgm = Mathf.Lerp(-80f,bgmVol, time / interval);
            mixer.SetFloat("BGVol", bgm);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
