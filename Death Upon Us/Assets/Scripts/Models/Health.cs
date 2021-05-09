using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	public Slider slider;
	public Gradient gradient;
	public Image fill;

	private int healthValue;

	private void Start() {
		SetMaxHealth(100);
	}

	/// <summary>
    /// Sets the max health value to be taken into account in the slider range.
    /// </summary>
	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;

		fill.color = gradient.Evaluate(1f);
		healthValue = health;
	}

	/// <summary>
    /// Sets the current value of the slider.
    /// </summary>
    public void UpdateHealthUI()
	{
		slider.value = healthValue;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

    public void ChangeHealth(int healthDelta)
	{
		Debug.Log(healthValue);
		healthValue += healthDelta;
		Debug.Log(healthValue);
		UpdateHealthUI();
	}
}