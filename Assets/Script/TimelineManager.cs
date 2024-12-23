using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : Singleton<TimelineManager>
{
    public PlayableDirector[] director;
    public GameObject[] timeline;

    void Start()
    {
    }

    void AllInit()
    {
        for (int i = 0; i < timeline.Length; i++)
        {
            timeline[i].SetActive(false);
        }
    }

    public void PlayTimeline(int number)
    {
        AllInit();

        timeline[number].SetActive(true);
        director[number].Play();
    }

    public void StopTimeline(int number)
    {
        director[number].Stop();
    }
}
