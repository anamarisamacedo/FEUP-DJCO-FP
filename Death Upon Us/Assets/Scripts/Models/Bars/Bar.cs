using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private int value;

    private bool flashing;

    private void Start()
    {
        SetMaxValue(100);
    }

    /// <summary>
    /// Sets the max value in the slider range.
    /// </summary>
    public void SetMaxValue(int value)
    {
        slider.maxValue = value;
        slider.value = value;

        fill.color = gradient.Evaluate(1f);
        this.value = value;
    }

    public int GetValue()
    {
        return value;
    }

    public void ChangeValue(int deltaValue)
    {
        value += deltaValue;
        UpdateUI();
    }

    public void UpdateUI()
    {
        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        if (value / slider.maxValue < 0.25f)
        {
            if (!flashing)
            {
                InvokeRepeating("Flash", 0, 0.3f);
                flashing = true;
            }
        }
        else
        {
            CancelInvoke();
            gameObject.SetActive(true);
            flashing = false;
        }
    }

    void Flash()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}