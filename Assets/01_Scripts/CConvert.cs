using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CConvert : MonoBehaviour
{
	Image img;

	public Sprite whiteImage;
	public Sprite BlackImage;

	private void Awake()
	{
		img = GetComponentInChildren<Image>();
	}

	private void Update()
	{
		if (GameManager.instance.pinven.stat == PlayerForm.Magic)
		{
			img.sprite = BlackImage;
		}
		else if (GameManager.instance.pinven.stat == PlayerForm.Yoho)
		{
			img.sprite = whiteImage;
		}
	}
}
