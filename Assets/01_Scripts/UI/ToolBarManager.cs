using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum toolState
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
    public toolState state;

	public GameObject Inventory;
	public GameObject Medicine;
	public GameObject Quest;
	public GameObject Node;
	public GameObject Setting;

	public GameObject InventoryBtn;
	public GameObject MedicineBtn;
	public GameObject QuestBtn;
	public GameObject NodeBtn;
	public GameObject SettingBtn;

	private void Start()
	{
		
	}

	private void Update()
	{
		if(InventoryBtn.GetComponent<ToolBtn>().isTarget)
		{
			ToolOff();
			state = toolState.Inventory;
		}
		else if (MedicineBtn.GetComponent<ToolBtn>().isTarget)
		{
			ToolOff();
			state = toolState.Medicine;
		}
		else if (QuestBtn.GetComponent<ToolBtn>().isTarget)
		{
			ToolOff();
			state = toolState.Quest;
		}
		else if (NodeBtn.GetComponent<ToolBtn>().isTarget)
		{
			ToolOff();
			state = toolState.Node;
		}
		else if(SettingBtn.GetComponent<ToolBtn>().isTarget)
		{
			ToolOff();
			state = toolState.Setting;
		}
		else
		{
			state = toolState.None;
		}

		switch (state)
		{
			case toolState.Inventory:
				Inventory.SetActive(true);
				break;
			case toolState.Medicine:
				Medicine.SetActive(true);
				break;

			case toolState.Quest:
				Quest.SetActive(true);
				break;

			case toolState.Node:
				Node.SetActive(true);
				break;

			case toolState.Setting:
				Setting.SetActive(true);
                break;

			case toolState.None:
				ToolOff();
				//BtnOff();
				break;
		}
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
		InventoryBtn.GetComponent<ToolBtn>().Exit();
		InventoryBtn.GetComponent<ToolBtn>().isTarget  = false;

		MedicineBtn.GetComponent<ToolBtn>().Exit();
		MedicineBtn.GetComponent<ToolBtn>().isTarget = false;

		QuestBtn.GetComponent<ToolBtn>().Exit();
		QuestBtn.GetComponent<ToolBtn>().isTarget = false;

		NodeBtn.GetComponent<ToolBtn>().Exit();
		NodeBtn.GetComponent<ToolBtn>().isTarget = false;

		SettingBtn.GetComponent<ToolBtn>().Exit();
		SettingBtn.GetComponent<ToolBtn>().isTarget  = false;
	}

}
