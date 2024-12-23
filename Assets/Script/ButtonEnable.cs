using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEnable : MonoBehaviour
{
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void ClickEnable()
    {
        button.interactable = false;
    }
}
