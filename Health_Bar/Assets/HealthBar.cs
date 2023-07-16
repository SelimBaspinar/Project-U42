using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public Slider slider;
	public Gradient gradient;
	public Image fill;

	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;

		fill.color = gradient.Evaluate(1f);
		// saðlýk maximumdayken istenilen renk yeþil olmasý için ve 0'dan 1'e kadar olan bi sayý atamasý için "1f" 
	}

	public void SetHealth(int health)
	{
		slider.value = health;

		fill.color = gradient.Evaluate(slider.normalizedValue);
		// maximum deðerin deðiþebilir olmasý için "normalizedValue"
	}

}
