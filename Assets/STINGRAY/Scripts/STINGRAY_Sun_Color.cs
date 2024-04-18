using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

[ExecuteInEditMode]
public class STINGRAY_Sun_Color : MonoBehaviour
{
    Light _light;
    [SerializeField] private float sunIntensity = 3;
    //[SerializeField] private Gradient dayToEveningGradient;
    [SerializeField] private Color dayColour;
    [SerializeField] private Color eveningColour;

	//fog
	[SerializeField] private bool controlFog = false;
	[SerializeField] private Color orginalFogColor;

	[SerializeField] private bool timeCycle = false;
	[SerializeField] private float timeCycleTime = 1.0f;
	[SerializeField] private float startAngle = 20.0f;

	public float currentTime = 0.0f;
	private void Awake()
	{
		//orginalFogColor = RenderSettings.fogColor;
		transform.eulerAngles = new Vector3(startAngle, 20.0f, 0.0f);
	}

	void Update()
    {
        if(_light == null)
        {
            _light = GetComponent<Light>();
        }

        float dotProduct = Vector3.Dot(-transform.forward, Vector3.up);
        float clampedDot = Mathf.Clamp((dotProduct + 0.9f), 0, 1);
        float topDot = (1 - Mathf.Clamp01(dotProduct)) * Mathf.Clamp01(Mathf.Sign(dotProduct));
        float bottomDot = (1 - Mathf.Clamp01(-dotProduct)) * Mathf.Clamp01(Mathf.Sign(-dotProduct));
        topDot = Mathf.Pow(math.smoothstep(0f, 0.9f, topDot), 5);
        bottomDot = Mathf.Pow(bottomDot, 5);

        _light.intensity = Mathf.Lerp(0.1f, sunIntensity, Mathf.Pow(clampedDot, 5));
        _light.color = Color.Lerp(dayColour, eveningColour, topDot + bottomDot);

        if(transform.localEulerAngles.x == -90)
        {
            transform.localEulerAngles = new Vector3(-89.9f, transform.eulerAngles.y, transform.eulerAngles.z);
        }

		if (timeCycle)
		{
			transform.Rotate(Vector3.right, timeCycleTime * Time.deltaTime);
			//float v = transform.eulerAngles.x + timeCycleTime * Time.deltaTime;
			//if (v > 360) v -= 360; //수정필요
			//transform.eulerAngles = new Vector3(v, 20, 0);

			//currentTime = v / 15f;
		}
		else transform.eulerAngles = new Vector3(startAngle, 20, 0);

		if(controlFog)
		{
			if (_light.intensity < 5)
			{
				RenderSettings.fogColor = orginalFogColor * (_light.intensity * 0.2f);
			}
			else RenderSettings.fogColor = orginalFogColor;
		}
    }
}
