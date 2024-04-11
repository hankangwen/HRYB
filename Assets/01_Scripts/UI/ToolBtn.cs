using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.EditorTools;

public enum BtnState
{
	On,
	Off,
	Reset
}

public class ToolBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	private Image img;
	public Sprite BtnUI;
	public Sprite OnBtnUI;

	private TMP_Text text;

	public bool isTarget;

	public BtnState state;

	private void Awake()
	{
		isTarget = false;
		img = GetComponent<Image>();
		text = GetComponentInChildren<TMP_Text>();
	}

	private void Start()
	{
		img.sprite = BtnUI;
		text.color = Color.white;
	}

	public void Enter()
	{
		state = BtnState.On;
	}

	public void Exit()
	{
		state = BtnState.Off;
		Debug.Log("올라감!");
	}

	public void Click()
	{
		GetComponentInParent<ToolBarManager>().BtnOff();
		isTarget = true;
	}

	private void Update()
	{
		switch(state)
		{
			case BtnState.On:
				img.sprite = OnBtnUI;
				text.color = Color.black;
				break;

			case BtnState.Off:
				img.sprite = BtnUI;
				text.color = Color.white;
				break;

			case BtnState.Reset:
				isTarget = false;
			    break;


		}
		if (isTarget)
		{
			img.sprite = OnBtnUI;
			text.color = Color.black;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		state = BtnState.On;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		state = BtnState.Off;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Click();
	}
}
