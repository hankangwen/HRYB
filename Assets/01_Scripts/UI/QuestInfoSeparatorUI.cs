using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestInfoSeparatorUI : MonoBehaviour
{
	TextMeshProUGUI sectionTxt;
	public void ShowInfo(QuestType t)
	{
		if(sectionTxt == null)
		{
			sectionTxt = GetComponentInChildren<TextMeshProUGUI>();
		}
		switch (t)
		{
			case QuestType.Main:
				sectionTxt.text = "메인 퀘스트";
				break;
			case QuestType.Sub:
				sectionTxt.text = "서브 퀘스트";
				break;
		}
	}
}
