using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestConditionUI : MonoBehaviour
{
    TextMeshProUGUI condText;
	TextMeshProUGUI compCount;

	public void ShowCond(CompleteAtom comp)
	{
		if(condText == null)
		{
			condText = transform.Find("").GetComponent<TextMeshProUGUI>();
		}
		if(compCount == null)
		{
			compCount = transform.Find("").GetComponent<TextMeshProUGUI>();
		}

		System.Text.StringBuilder sb;
		bool usingGlobal = GameManager.GetGlobalSB(out sb);

		sb.Append("<#00dd00>");
		sb.Append(comp.parameter);
		sb.Append("</color>을(를) ");

		

		GameManager.ReturnGlobalSB(usingGlobal);
	}
}
