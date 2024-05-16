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
	

	public void ShowInfo(RewardAtom rew)
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

		switch (rew.rewardType)
		{
			case RewardType.Exp:
				//typeImg.sprite = GameManager.instance.expSprite;
				break;
			case RewardType.Skill:
				typeImg.sprite = GameManager.instance.skillLoader.GetSkill(rew.parameter).skillIcon;
				break;
			case RewardType.Item:
				typeImg.sprite = Item.GetItem<Item>(rew.parameter).icon;
				break;
			case RewardType.HealWhite:
			case RewardType.HealBlack:
			case RewardType.Quest:
				return;
		}

		System.Text.StringBuilder sb;
		bool usingGlobal = GameManager.GetGlobalSB(out sb);

		rewNameTxt.text = rew.parameter;

		sb.Append("<#00dd00>");
		sb.Append(rew.amount);
		sb.Append("</color>");
		sb.Append(" 개");
		rewAmtTxt.text = sb.ToString();
		sb.Clear();

		GameManager.ReturnGlobalSB(usingGlobal);
	}
}

