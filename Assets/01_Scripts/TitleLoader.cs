using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Drawing;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.SceneManagement;

public class TitleLoader : MonoBehaviour
{
	TMP_Text titleText;
	Image backgroundImg;

	[Header("실행 시간")]
	public float loadTime;
	float time = 0;


	private void Awake()
	{
		titleText = GetComponentInChildren<TMP_Text>();
		backgroundImg = GetComponentInChildren<Image>();
	}

	void Start()
    {
		titleText.color = new UnityEngine.Color(titleText.color.r, titleText.color.g, titleText.color.b, 0);
		backgroundImg.color = new UnityEngine.Color(backgroundImg.color.r, backgroundImg.color.g, backgroundImg.color.b, 0);
    }

	public void Fade()
	{
		StartCoroutine(FadeInOut());
	}

	public IEnumerator FadeInOut()
	{
		time = 0;
		UnityEngine.Color backA = backgroundImg.color;
		UnityEngine.Color textA = titleText.color;
		while (backA.a < 1)
		{
			time += Time.deltaTime / (loadTime / 2);
			backA.a = Mathf.Lerp(0, 1, time);
			textA.a = Mathf.Lerp(0, 1, time);
			backgroundImg.color = backA;
			backgroundImg.color = textA;
		}
		while (backA.a > 0)
		{
			time += Time.deltaTime / (loadTime / 2);
			backA.a = Mathf.Lerp(1, 0, time);
			textA.a = Mathf.Lerp(1, 0, time);
			backgroundImg.color = backA;
			backgroundImg.color = textA;
			yield return null;
		}
	}
}
