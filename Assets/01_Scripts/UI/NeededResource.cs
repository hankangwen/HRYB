using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NeededResource : MonoBehaviour
{
	TextMeshProUGUI numberText;



	public void SetInfo(int requirement)
	{
		if(numberText == null)
		{
			numberText = transform.Find("ResourceAmount").GetComponent<TextMeshProUGUI>();
		}

		System.Text.StringBuilder sb;
		bool usingGlobal = GameManager.GetGlobalSB(out sb);
		if (GameManager.instance.pinven.currentExp >= requirement)
		{
			sb.Append("<#00dd00>");
			sb.Append(GameManager.instance.pinven.currentExp);
			sb.Append("</color> / ");
			sb.Append(requirement);
		}
		else
		{
			sb.Append("<#dd0000>");
			sb.Append(GameManager.instance.pinven.currentExp);
			sb.Append("</color> / ");
			sb.Append(requirement);
		}
		numberText.text = sb.ToString();
		GameManager.ReturnGlobalSB(usingGlobal);
	}
}
