using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestRewardUI : MonoBehaviour
{

	Image typeImg;
	TextMeshProUGUI rewNameTxt;
	TextMeshProUGUI rewAmtTxt;
	

	public void Refresh(RewardAtom rew)
	{
		if(typeImg == null)
		{
			typeImg = transform.Find("RewIconMask/RewIcon").GetComponent<Image>();
		}
		if (rewNameTxt == null)
		{
			rewNameTxt = transform.Find("RewName").GetComponent<TextMeshProUGUI>();
		}
		if (rewAmtTxt == null)
		{
			rewAmtTxt = transform.Find("RewItemAmount").GetComponent<TextMeshProUGUI>();
		}

		System.Text.StringBuilder sb;
		bool usingGlobal = GameManager.GetGlobalSB(out sb);

		rewNameTxt.text = rew.parameter;

		sb.Append("<#00dd00>");
		sb.Append(rew.parameter);
		sb.Append("</color>");
		sb.Append(" 개");
		rewAmtTxt.text = sb.ToString();
		sb.Clear();

		GameManager.ReturnGlobalSB(usingGlobal);
	}
}

