using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IOpenableWindowUI
{
	public int quickInven;

	[SerializeField]
	private SlotUI[] slotUI;

	[SerializeField]
	private DragHandler[] dragHandler;

	private void Awake()
	{
		slotUI = GetComponentsInChildren<SlotUI>();
		dragHandler = GetComponentsInChildren<DragHandler>();

		for (int i = 0; i < GameManager.instance.pinven.cap; i++)
		{
			slotUI[i].value = i;
			dragHandler[i].value = i;
		}
	}

	public void OnClose()
	{
		//throw new System.NotImplementedException();
	}

	public void OnOpen()
	{
		for (int i = 0; i < GameManager.instance.pinven.cap; i++)
		{
			slotUI[i].value = i;
			dragHandler[i].value = i;
		}
		GameManager.instance.uiManager.UpdateInvenUI();
	}

	public void WhileOpening()
	{
		//throw new System.NotImplementedException();
	}
}
