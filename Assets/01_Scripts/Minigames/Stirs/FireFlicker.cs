using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlicker : MonoBehaviour
{
    public float minFlicker;
	public float maxFlicker;

	public float flickerTime;

	float objective;
	float prevIntensity;

	float t;

	Light lht;

	private void Awake()
	{
		lht = GetComponent<Light>();
	}

	private void Update()
	{
		t += Time.unscaledDeltaTime;

		if(flickerTime <= t)
		{
			t = 0;
			objective = Random.Range(minFlicker, maxFlicker);
			prevIntensity = lht.intensity;
		}
		else
		{
			lht.intensity = Mathf.Lerp(prevIntensity, objective, t/flickerTime);
		}
	}
}
