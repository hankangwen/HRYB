using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeLearnUI : MonoBehaviour
{
	PlayerNode showing;
	bool isOn = false;
	Transform scroller;
	public const float SCROLLMAX = 1030;
	Coroutine ongoing;

	TextMeshProUGUI title;
	NeededResource req;

	private void Awake()
	{
		scroller = transform.Find("Scroller");
		title = scroller.Find("Names/NodeName").GetComponent<TextMeshProUGUI>();
		req = scroller.Find("Requires/NeededResource").GetComponent<NeededResource>();
	}

	private void Start()
	{
		gameObject.SetActive(false);
	}

	public void On(PlayerNode node)
	{
		if (!isOn && ongoing == null)
		{
			gameObject.SetActive(true);
			showing = node;
			ongoing = StartCoroutine(DelScroll(true));
			
		}

		RefreshInfo();
	}

	public void Off()
	{
		if (isOn && ongoing == null)
		{
			showing = null;
			ongoing = GameManager.instance.StartCoroutine(DelScroll(false));
		}
		

	}

	public void Learn()
	{
		if (isOn)
		{
			showing.LearnNode();
		}
	}

	public void RefreshInfo()
	{
		if(!showing)
			return;
		req.SetInfo(((int)showing.needPoint));
		title.text = NodeUtility.GetName(showing);
	}

	public IEnumerator DelScroll(bool direction)
	{
		float t = 0;
		float accOffset;
		Vector3 originalPos = scroller.transform.position;
		while(t < NodeViewer.MOVESEC)
		{
			yield return null;
			t += Time.deltaTime;
			accOffset = Mathf.Lerp(0, SCROLLMAX, t / NodeViewer.MOVESEC);
			scroller.transform.position = originalPos + (direction ? Vector3.down : Vector3.up) * accOffset;
		}
		scroller.position = originalPos + (direction ? Vector3.down : Vector3.up) * SCROLLMAX;

		isOn = direction;
		ongoing = null;
		gameObject.SetActive(isOn);
	}

	public void OnOff(PlayerNode node)
	{
		if (!isOn)
		{
			(GameManager.instance.uiManager.toolbarUIShower.openables[ToolState.Node] as NodeViewer)?.ShowLearner(node);	
		}
		else
		{
			if(showing == node)
			{
				(GameManager.instance.uiManager.toolbarUIShower.openables[ToolState.Node] as NodeViewer)?.UnshowLearner();
			}
			else
			{
				RefreshInfo();
			}
		}
	}
}
