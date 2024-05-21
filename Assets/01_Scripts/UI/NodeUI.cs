using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NodeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
	PlayerNode indicating;

	Button button;
	Image img;
	public void SetUpNodeUI(PlayerNode node)
	{
		button = GetComponent<Button>();
		img = GetComponent<Image>();
		indicating = node;
		switch (indicating.nodeType) //여기서 이미지를 정해주든 뭐든
		{
			case StatUpgradeType.White:
				//양
				break;
			case StatUpgradeType.Black:
				//음
				break;
			case StatUpgradeType.WhiteAtk:
				//양공격력
				break;
			case StatUpgradeType.BlackAtk:
				//음공격력
				break;
			case StatUpgradeType.MoveSpeed:
				//이속
				break;
			case StatUpgradeType.CooldownRdc:
				//쿨감?
				break;
			default:
				break;
		}

		button.onClick.AddListener(()=>{ indicating.LearnNode(); });
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		GameManager.instance.uiManager.detailer.ShowDetail(indicating, eventData.position);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		GameManager.instance.uiManager.detailer.OffDetail();
	}

	public void OnPointerMove(PointerEventData eventData)
	{
		GameManager.instance.uiManager.detailer.transform.position = eventData.position;
	}
}
