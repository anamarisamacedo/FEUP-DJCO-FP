using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_Input_Button : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button okButton;

    private void Awake()
    {
        button1 = transform.Find("Button1").GetComponent<Button>();
        button2 = transform.Find("Button2").GetComponent<Button>();
        button3 = transform.Find("Button3").GetComponent<Button>();
        button4 = transform.Find("Button4").GetComponent<Button>();
        button5 = transform.Find("Button5").GetComponent<Button>();
        okButton = transform.Find("Button6").GetComponent<Button>();

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
