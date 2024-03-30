using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleDetected : MonoBehaviour
{
	public string text;
	public TitleLoader loader;


	private void Start()
	{ 
	}

	private void OnTriggerEnter(Collider other)
	{
		loader.Fade();
	}
}
