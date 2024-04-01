using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CConvert : MonoBehaviour
{
	Image coolDown;
	Image img;

	public Sprite whiteImage;
	public Sprite BlackImage;
		
	float curCool;
	float time;

	bool isForm;

	private void Awake()
	{
		img = transform.GetChild(0).GetComponent<Image>();
		coolDown = transform.GetChild(1).GetComponent<Image>();
		time = GameManager.instance.pinven.changeCool;
	}

	private void Update()
	{
		if (GameManager.instance.pinven.stat == PlayerForm.Magic && !isForm)
		{
			time = 0;
			isForm = true;
			img.sprite = BlackImage;
		}
		else if (GameManager.instance.pinven.stat == PlayerForm.Magic && isForm)
		{
			img.sprite = BlackImage;
		}
		else if (GameManager.instance.pinven.stat == PlayerForm.Yoho && isForm)
		{
			time = 0;
			isForm = false;
			img.sprite = whiteImage;
		}
		else if (GameManager.instance.pinven.stat == PlayerForm.Magic && !isForm)
		{
			img.sprite = BlackImage;
		}

		time += Time.deltaTime;
		curCool = 1 - time / GameManager.instance.pinven.changeCool;

		coolDown.fillAmount = curCool;
	}
}
