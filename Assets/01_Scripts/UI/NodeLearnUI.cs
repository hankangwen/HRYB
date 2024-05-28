using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLearnUI : MonoBehaviour
{
	PlayerNode showing;
	bool isOn;
	Transform scroller;
	public const float SCROLLMAX = 450;

	private void Awake()
	{
		scroller = transform.Find("Scroller");
	}

	public void On(PlayerNode node)
	{
		gameObject.SetActive(true);
		showing = node;
		StartCoroutine(DelScroll(true));

		//정보설정
	}

	public void Off()
	{
		gameObject.SetActive(false);
		showing = null;
		StartCoroutine(DelScroll(false));

	}

	public void Learn()
	{
		if (isOn)
		{
			showing.LearnNode();
		}
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
	}

	public void OnOff(PlayerNode node)
	{
		if (isOn)
		{
			(GameManager.instance.uiManager.toolbarUIShower.openables[ToolState.Node] as NodeViewer)?.ShowLearner(node);
		}
		else
		{
			(GameManager.instance.uiManager.toolbarUIShower.openables[ToolState.Node] as NodeViewer)?.UnshowLearner();
		}
	}
}
