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

	private void Start() {
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
	}
}