using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    bool mainGame;

    [SerializeField] GameObject[] canvas;

    [SerializeField] GameObject focusMenu;

    void Start()
    {
        CanvasInit();
        canvas[0].SetActive(true);

        mainGame = true;
        Time.timeScale = 1;
    }

    void FixedUpdate()
    {
        PauseChange();
    }

    void CanvasInit()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(false);
        }
    }

    void PauseChange()
    {
        if (mainGame)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                CanvasInit();
                canvas[1].SetActive(true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                CanvasInit();
                canvas[0].SetActive(true);
            }
        }
    }
}
