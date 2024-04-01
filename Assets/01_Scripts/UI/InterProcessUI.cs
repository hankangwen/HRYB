using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterProcessUI : MonoBehaviour
{
    public Slider gauge;
	public Color chargingColor;

	public Color chargedColor;

	bool isOn = true;

	Image fill;

	private void Awake()
	{
		gauge = GetComponent<Slider>();
		fill = gauge.transform.Find("Fill Area/Fill").GetComponent<Image>();
	}

	public void SetGaugeValue(float v)
	{
		gauge.value = v;
		if(v >= 1)
		{
			fill.color = chargedColor;
		}
		else
		{
			fill.color = chargingColor;
		}
	}

	public void On()
	{
		if (!isOn)
		{
			isOn = true;
			gameObject.SetActive(true);
		}
	}

	public void Off()
	{
		if (isOn)
		{
			isOn = false;
			gameObject.SetActive(false);
		}
	}
}
