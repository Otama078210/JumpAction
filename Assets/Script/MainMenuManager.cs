using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;


public class MainMenuManager : Singleton<MainMenuManager>
{
    public GameObject[] canvas;

    public GameObject[] focusObject;

    GameObject currentFocus;
    GameObject previousFocus;

    int pauseMenuNum = 0;

    void Start()
    {
        Time.timeScale = 1;
        SoundManager.Instance.VolumeChange(4, 5);

        CanvasInit();
        CanvasCheck();

        Debug.Log(pauseMenuNum);

        EventSystem.current.SetSelectedGameObject(focusObject[0]);

        if(SceneManager.GetActiveScene().name == "Title")
        {
            Transition_Menu(0);
            SoundManager.Instance.PlayBGM(0);
        }
    }

    void Update()
    {
        FocusCheck();

        if (GameObject.Find("GameManager") != null)
        {
            if (GameManager.Instance.mainGame)
            {
                PauseMenu();
            }
        }
    }

    void CanvasCheck()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            if (canvas[i].name == "Canvas_Pause")
            {
                pauseMenuNum = i;
            }
        }
    }

    public void CanvasInit()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(false);
        }
    }

    public void Transition_Menu(int nextMenu)
    {
        CanvasInit();

        canvas[nextMenu].SetActive(true);

        EventSystem.current.SetSelectedGameObject(focusObject[nextMenu]);
    }

    void FocusCheck()
    {
        currentFocus = EventSystem.current.currentSelectedGameObject;

        if (currentFocus == previousFocus) return;

        if (currentFocus == null)
        {
            EventSystem.current.SetSelectedGameObject(previousFocus);
            return;
        }

        previousFocus = EventSystem.current.currentSelectedGameObject;
    }

    public void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !canvas[pauseMenuNum].activeSelf == true)
        {
            canvas[pauseMenuNum].SetActive(true);

            SoundManager.Instance.PlaySE_Sys(5);

            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && canvas[pauseMenuNum].activeSelf == true)
        {
            canvas[pauseMenuNum].SetActive(false);

            SoundManager.Instance.PlaySE_Sys(4);

            Time.timeScale = 1;
        }

            if (!canvas[pauseMenuNum].activeSelf == true)
        {
            Time.timeScale = 1;
        }
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
