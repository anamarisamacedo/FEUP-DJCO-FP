using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UI_Input_Field : MonoBehaviour
{
    public InputField inputField;
    private void Awake()
    {
        inputField = transform.Find("InputField").GetComponent<InputField>();
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
