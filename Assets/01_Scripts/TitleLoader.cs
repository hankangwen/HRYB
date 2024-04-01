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
	float fadeInOutTime;

	private void Awake()
	{
		titleText = GetComponentInChildren<TMP_Text>();
		canvasGroup = GetComponent<CanvasGroup>();
	}

	void Start()
	{
		canvasGroup.alpha = 0f;
	}

	public void FadeInOut(string text, float time)
	{
		if (isFade) return;

		fadeInOutTime = time;
		isFade = true;
		titleText.text = text;

		GameManager.instance.audioPlayer.PlayPoint("TextOn", transform.position);

		StartCoroutine(FadeInOutRoutine());
	}

	IEnumerator FadeInOutRoutine()
	{
		float elapsedTime = 0f;

		// Fade In
		while (elapsedTime <= fadeInOutTime)
		{
			elapsedTime += Time.deltaTime;
			canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeInOutTime);
			yield return null;
		}

		canvasGroup.alpha = 1f;

		// Wait for a moment
		yield return new WaitForSeconds(1f);

		// Fade Out
		elapsedTime = fadeInOutTime; // Reset elapsed time for fade out
		while (elapsedTime >= 0f)
		{
			elapsedTime -= Time.deltaTime;
			canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeInOutTime);
			yield return null;
		}

		canvasGroup.alpha = 0f;
		isFade = false;
	}
}
