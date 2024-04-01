using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleDetected : MonoBehaviour
{
	public string text;
	public TitleLoader loader;

	private void Start()
	{
		loader = GameObject.Find("TitleLoad").GetComponent<TitleLoader>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == 6)
		{
			Debug.LogError("으악 사람이다!");
			loader.FadeInOut(text, 0.5f);
			
		}
	}
}
