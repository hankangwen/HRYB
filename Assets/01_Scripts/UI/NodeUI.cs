using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NodeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
	public PlayerNode indicating;

	Button button;
	Image img;
	Image circleIndicator;
	Coroutine ongoing;

	private void Start()
	{
		button = GetComponent<Button>();
		img = GetComponent<Image>();
		circleIndicator = transform.Find("CircleIndicator").GetComponent<Image>();
		switch (indicating?.nodeType) //여기서 이미지를 정해주든 뭐든
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
		circleIndicator.fillAmount = 0;
		button.onClick.AddListener(() => { (GameManager.instance.uiManager.toolbarUIShower.openables[ToolState.Node] as NodeViewer)?.nodeLearner.OnOff(indicating);});
		button.onClick.AddListener(() => { (GameManager.instance.uiManager.toolbarUIShower.openables[ToolState.Node] as NodeViewer)?.SetSelected(this); });
	}

	public void BrushStroke()
	{
		circleIndicator.enabled = true;
		
		ongoing = StartCoroutine(DelStroke());
	}

	public void OffBrush()
	{
		circleIndicator.enabled = false;
	}

	IEnumerator DelStroke()
	{
		float t = 0;
		circleIndicator.fillAmount = 0;
		while (t < NodeViewer.CIRCLESEC)
		{
			yield return null;
			t += Time.deltaTime;
			circleIndicator.fillAmount = Mathf.Lerp(0, 1, t / NodeViewer.CIRCLESEC);
		}
	}

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
