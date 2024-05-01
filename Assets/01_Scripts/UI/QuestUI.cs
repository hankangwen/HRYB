using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour, IOpenableWindowUI
{
	QuestDetailUI questInfo;
	QuestListUI questList;
	public void OnClose()
	{

	}

	public void OnOpen()
	{
		
	}

	public void Refresh()
	{
		questInfo.RefreshCompInfo();
		questList.RefreshQuestUIState();
	}

	public void RefreshQuestListCompletely()
	{
		questList.RefreshAvailQuests();
	}

	public void WhileOpening()
	{

	}

	void Awake()
    {
        questInfo = GetComponentInChildren<QuestDetailUI>();
		questList = GetComponentInChildren<QuestListUI>();
    }

	public void ShowInfo(QuestInfo inf)
	{
		questInfo.ShowInfoOf(inf);
	}
}
