using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodePartSelector : MonoBehaviour
{
	public BodyPart part;

	GameObject obj;
	private void Awake()
	{
		obj = GameObject.Find($"{part.ToString()}Bgnd");
	}

	public void OnClick()
	{
		obj.SetActive(true);
		transform.parent.gameObject.SetActive(false);
	}
}
