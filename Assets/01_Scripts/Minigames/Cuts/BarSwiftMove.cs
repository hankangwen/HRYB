using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarSwiftMove : MonoBehaviour
{
    public float barOnceMoveTime;

	float accT;
	
	Scrollbar scr;

	private void Awake()
	{
		scr = GetComponent<Scrollbar>();
	}

	private void Update()
	{
		scr.value = Mathf.Lerp(0, 1, accT / barOnceMoveTime);

	}
}
