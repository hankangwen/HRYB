using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedicineButtonsUI : MonoBehaviour
{
    Button fryButton;
	Button stirButton;
	Button mashButton;
	Button cutButton;

	bool inited = false;

	private void Awake()
	{
		fryButton = transform.Find("Fry").GetComponent<Button>();
		stirButton = transform.Find("Stir").GetComponent<Button>();
		mashButton = transform.Find("Mash").GetComponent<Button>();
		cutButton = transform.Find("Cut").GetComponent<Button>();

		inited = false;
	}

	private void Start()
	{
		if (!inited)
		{
			fryButton.onClick.AddListener(() => GameManager.instance.LoadMinigame(Minigames.Frying));
			stirButton.onClick.AddListener(() => GameManager.instance.LoadMinigame(Minigames.Stirring));
			mashButton.onClick.AddListener(() => GameManager.instance.LoadMinigame(Minigames.Mashing));
			cutButton.onClick.AddListener(() => GameManager.instance.LoadMinigame(Minigames.Cutting));
		}

		SetStatuses(GameManager.instance.pinven.CurHoldingItem.info);
	}


	public void SetStatuses(Item itemStat)
	{
		if(itemStat is YinyangItem yy)
		{
			fryButton.interactable = !(yy.processes.Contains(ProcessType.Fry) || yy.processes.Contains(ProcessType.Burn));
			stirButton.interactable = !yy.processes.Contains(ProcessType.Stir);
			mashButton.interactable = !yy.processes.Contains(ProcessType.Mash);
			cutButton.interactable = Crafter.recipeItemTableTrim.ContainsKey(new ItemAmountPair(yy));
		}
		else
		{
			fryButton.interactable = false;
			stirButton.interactable = false;
			mashButton.interactable = false;
			cutButton.interactable = false;
		}
	}
}
