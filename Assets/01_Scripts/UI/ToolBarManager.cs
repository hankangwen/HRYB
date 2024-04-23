using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolState
{
	Inventory,
	Medicine,
	Quest,
	Node,
	Setting,
	None
}
public class ToolBarManager : MonoBehaviour
{
    public ToolState state;

	public GameObject Inventory;
	public GameObject Medicine;
	public GameObject Quest;
	public GameObject Node;
	public GameObject Setting;

	public ToolBtn InventoryBtn;
	public ToolBtn MedicineBtn;
	public ToolBtn QuestBtn;
	public ToolBtn NodeBtn;
	public ToolBtn SettingBtn;

	IOpenableWindowUI curOpened;

	IOpenableWindowUI invenWindow;
	IOpenableWindowUI medicineWindow;
	IOpenableWindowUI questWindow;
	IOpenableWindowUI nodeWindow;
	IOpenableWindowUI settingWindow;

	private void Awake()
	{

		invenWindow = Inventory.GetComponent<IOpenableWindowUI>();
		
		medicineWindow = Medicine.GetComponent<IOpenableWindowUI>();
		questWindow = Quest.GetComponent<IOpenableWindowUI>();
		nodeWindow = Node.GetComponent<IOpenableWindowUI>();
		settingWindow = Setting.GetComponent<IOpenableWindowUI>();
	}

	private void Start()
	{
		ChangeStatus(ToolState.Inventory);
	}

	private void Update()
	{
		if (curOpened != null)
		{
			curOpened.WhileOpening();
		}
		
	}

	public void ChangeStatus(ToolState windowStat)
	{
		ToolOff();
		BtnOff();
		if (curOpened != null)
		{
			curOpened.OnClose();
		}
		state = windowStat;
		switch (state)
		{
			case ToolState.Inventory:
				Inventory.SetActive(true);
				InventoryBtn.Lighten();
				if (invenWindow != null)
				{
					curOpened = invenWindow;
					curOpened.OnOpen();
				}
				else
				{
					curOpened = null;
				}
				break;
			case ToolState.Medicine:
				Medicine.SetActive(true);
				MedicineBtn.Lighten();
				if (medicineWindow != null)
				{
					curOpened = medicineWindow;
					curOpened.OnOpen();
				}
				else
				{
					curOpened = null;
				}
				break;

			case ToolState.Quest:
				Quest.SetActive(true);
				QuestBtn.Lighten();
				if (questWindow != null)
				{
					curOpened = questWindow;
					curOpened.OnOpen();
				}
				else
				{
					curOpened = null;
				}
				break;

			case ToolState.Node:
				Node.SetActive(true);
				NodeBtn.Lighten();
				if (nodeWindow != null)
				{
					curOpened = nodeWindow;
					curOpened.OnOpen();
				}
				else
				{
					curOpened = null;
				}
				break;

			case ToolState.Setting:
				Setting.SetActive(true);
				SettingBtn.Lighten();
				if (settingWindow != null)
				{
					curOpened = settingWindow;
					curOpened.OnOpen();
				}
				else
				{
					curOpened = null;
				}
				break;

			case ToolState.None:
				if (curOpened != null)
				{
					curOpened.OnClose();
				}
				curOpened = null;

				//BtnOff();
				break;
		}
	}

	public void CloseWindow()
	{
		ChangeStatus(ToolState.None);
	}

	void ToolOff()
	{
		Inventory.SetActive(false);
		Medicine.SetActive(false);
		Quest.SetActive(false);
		Node.SetActive(false);
		Setting.SetActive(false);
	}
	public void BtnOff()
	{
		switch (state)
		{
			case ToolState.Inventory:
				InventoryBtn.ResetButton();
				break;
			case ToolState.Medicine:
				MedicineBtn.ResetButton();
				break;
			case ToolState.Quest:
				QuestBtn.ResetButton();
				break;
			case ToolState.Node:
				NodeBtn.ResetButton();
				break;
			case ToolState.Setting:
				SettingBtn.ResetButton();
				break;
			default:
				break;
		}
	}

}
