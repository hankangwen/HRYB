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

		ToolOff();
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
		if (curOpened != null)
		{
			curOpened.OnClose();
		}
		state = windowStat;
		switch (state)
		{
			case ToolState.Inventory:
				Inventory.SetActive(true);
				if (invenWindow != null)
				{
					curOpened = invenWindow;
					curOpened.OnOpen();
				}
				break;
			case ToolState.Medicine:
				Medicine.SetActive(true);
				if (medicineWindow != null)
				{
					curOpened = medicineWindow;
					curOpened.OnOpen();
				}
				break;

			case ToolState.Quest:
				Quest.SetActive(true);
				if (questWindow != null)
				{
					curOpened = questWindow;
					curOpened.OnOpen();
				}
				break;

			case ToolState.Node:
				Node.SetActive(true);
				if (nodeWindow != null)
				{
					curOpened = nodeWindow;
					curOpened.OnOpen();
				}
				break;

			case ToolState.Setting:
				Setting.SetActive(true);
				if (settingWindow != null)
				{
					curOpened = settingWindow;
					curOpened.OnOpen();
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
		InventoryBtn.Exit();

		MedicineBtn.Exit();

		QuestBtn.Exit();

		NodeBtn.Exit();

		SettingBtn.Exit();
	}

}
