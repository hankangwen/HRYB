using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleLoader : MonoBehaviour
{
	TMP_Text titleText;
	CanvasGroup canvasGroup;

	public bool isFade = false;

	[Header("실행 시간")]
	public float fadeInOutTime;


	private void Awake()
	{
		titleText = GetComponentInChildren<TMP_Text>();
		canvasGroup = GetComponent<CanvasGroup>();
	}

	void Start()
    {
		canvasGroup.alpha = 0f;
    }

	public void FadeInOut(string text)
	{
		isFade = true;
		Debug.LogError(canvasGroup);
		// Fade In

		titleText.text = text;
		float elapsedTime = 0f;
		float fadeSpeed = 1f / fadeInOutTime;

		while (elapsedTime < fadeInOutTime)
		{
			Debug.LogError(canvasGroup.alpha);
			canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime * fadeInOutTime);
			elapsedTime += Time.deltaTime;
		}

		canvasGroup.alpha = 1f;


		// Fade Out
		elapsedTime = 0f;

		while (elapsedTime < fadeInOutTime)
		{
			Debug.LogError(canvasGroup.alpha);
			canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime * fadeInOutTime);
			elapsedTime += Time.deltaTime;
		}

		canvasGroup.alpha = 0f;
		isFade = false;
	}
}
