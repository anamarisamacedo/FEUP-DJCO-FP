using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_Input_Button : MonoBehaviour
{
    public Button[] buttons;
    public Button okButton;
    private int nextButtonIndex;
    private bool[] selected = new bool[5];

    private void Start()
    {
        for (int i = 0; i < buttons.Length; ++i)
        {
            int buttonIndex = i; // Keep this line, it's essential
            string name = "Button" + (buttonIndex+1);
            buttons[buttonIndex] = transform.Find(name).GetComponent<Button>();
            selected[buttonIndex] = false;
        }
        okButton = transform.Find("Button6").GetComponent<Button>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Hide();
            Reset();
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            selected[0] = true;
            var colors = buttons[0].colors;
            colors.normalColor = Color.green;
            buttons[0].colors = colors;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            selected[1] = true;
            var colors = buttons[1].colors;
            colors.normalColor = Color.green;
            buttons[1].colors = colors;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            selected[2] = true;
            var colors = buttons[2].colors;
            colors.normalColor = Color.green;
            buttons[2].colors = colors;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            selected[3] = true;
            var colors = buttons[3].colors;
            colors.normalColor = Color.green;
            buttons[3].colors = colors;
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            selected[4] = true;
            var colors = buttons[4].colors;
            colors.normalColor = Color.green;
            buttons[4].colors = colors;
        }
    }

    private void Reset()
    {
        for (int i = 0; i < buttons.Length; ++i)
        {
            var colors = buttons[i].colors;
            colors.normalColor = Color.gray;
            buttons[i].colors = colors;
            selected[i] = false;
        }
    }

    private void Awake()
    {
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

    public bool[] GetSequence()
    {
        return this.selected;
    }
}
