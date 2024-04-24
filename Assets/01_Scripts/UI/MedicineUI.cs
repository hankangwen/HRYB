using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MedicineUI : MonoBehaviour, IOpenableWindowUI
{
	[SerializeField]
	private SlotUI[] slotUI;

	[SerializeField]
	List<int> medicineValue;

	private void Awake()
	{
		slotUI = GetComponentsInChildren<SlotUI>();

		for (int i = 0; i < GameManager.instance.pinven.cap; i++)
		{
			slotUI[i].value = -1;
		}
	}

	public void OnClose()
	{
		
	}

	public void OnOpen()
	{
		medicineValue.Clear();


		for (int i = 0; i < GameManager.instance.pinven.cap; i++)
		{
			if (GameManager.instance.pinven.inven[i].info is YinyangItem)
			{
				medicineValue.Add(i);
			}

		}

		for (int i = 0; i < medicineValue.Count; i++)
		{
			
			slotUI[i].value = medicineValue[i];
			
		}
		
		for(int i = 0; i < slotUI.Length; i++)
		{
			slotUI[i].UpdateItem();
		}

	}

	public void WhileOpening()
	{
		
	}
}
