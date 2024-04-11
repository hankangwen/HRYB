using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarSwiftMove : MonoBehaviour
{
    public float barMoveSpeed;

	public Vector2 barRandomJitter;
	
	Scrollbar scr;

	float accTime = 0;

	private void Awake()
	{
		scr = GetComponent<Scrollbar>();
		accTime = 0;
		ChangeSpeed();
	}

	private void Update()
	{
		accTime += Time.unscaledDeltaTime * barMoveSpeed;
		scr.value = Mathf.Lerp(0, 1, (Mathf.Sin(accTime) * 0.5f + 0.5f));

	}

	public void ChangeSpeed()
	{
		barMoveSpeed = Random.Range(barRandomJitter.x, barRandomJitter.y);
	}
}
