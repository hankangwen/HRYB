using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public enum BtnState
{
	On,
	Off,
	Reset,
	Focused
}

public class ToolBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private Image img;
	public Sprite BtnUI;
	public Sprite OnBtnUI;

	private TMP_Text text;

	public BtnState state;

	public ToolState indicating;

	private void Awake()
	{
		state = BtnState.Reset;
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
		img.sprite = OnBtnUI;
		text.color = Color.black;
	}

	public void Exit()
	{
		state = BtnState.Off;
		img.sprite = BtnUI;
		text.color = Color.white;
	}

	public void Click()
	{
		state = BtnState.Focused;
		img.sprite = OnBtnUI;
		text.color = Color.black;

		GameManager.instance.toolbarUIShower.ChangeStatus(indicating);
	}

	private void Update()
	{
		if (state == BtnState.Reset) //이건 어디서 되는지를 모르겠어서 일단 여기서 부름.
		{
			ResetButton();
		}
	}

	public void ResetButton()
	{
		state = BtnState.Off;
		img.sprite = BtnUI;
		text.color = Color.white;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Enter();
		Debug.Log("!@!@");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Exit();
	}
}
