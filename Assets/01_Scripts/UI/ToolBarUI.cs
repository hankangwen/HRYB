using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum toolState
{
	Inven,
	Quest,
	Medicine,
	Node,
	Setting,
}

public class ToolBarUI : MonoBehaviour
{
	toolState curState;
	List<Image> img;

	private void Start()
	{
		//img = GetComponentsInChildren<Image>();
	}

	private void Update()
	{
		
	}
}
