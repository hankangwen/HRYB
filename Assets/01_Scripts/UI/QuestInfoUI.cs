using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestInfoUI : MonoBehaviour
{
    TextMeshProUGUI questName;
	TextMeshProUGUI accompCount;

	Button selfButton;

	QuestInfo curShown;

	public void Refresh(QuestInfo inf)
	{
		if(questName == null)
		{
			questName = transform.Find("").GetComponent<TextMeshProUGUI>();
		}
		if (accompCount == null)
		{
			accompCount = transform.Find("").GetComponent<TextMeshProUGUI>();
		}
		if (selfButton == null)
		{
			selfButton = GetComponent<Button>();
		}
		curShown = inf;

		questName.text = inf.questName;
		System.Text.StringBuilder sb;
		bool usingGlobal = GameManager.GetGlobalSB(out sb);
		if (inf.IsDeprived)
		{
			sb.Append("<#00dd00>");
		}
		else
		{
			sb.Append("<#dd0000>");
		}
		sb.Append(inf.curCompletedAmount);
		sb.Append("</color> / ");
		sb.Append(inf.completableCount);

		GameManager.ReturnGlobalSB(usingGlobal);

	}

	public void OnClick()
	{
		GameManager.instance.uiManager.questUI.ShowInfo(curShown);
	}
}
