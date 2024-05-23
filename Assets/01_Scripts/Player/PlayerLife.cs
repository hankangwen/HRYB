using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : LifeModule 
{
	TitleLoader loader;
	public bool isFade = false;
	float fadeInOutTime;

	CanvasGroup fadeImg;

	Vector3[] spawnPoint = new Vector3[8];
	PlayerMove pMove;

	Vector3 initPos;

	public override bool isDead
	{
		get => yy.white.Value <= 0;
	}


	public override void Awake()
	{
		spawnPoint[0] = new Vector3(836, 14, 136);
		spawnPoint[1] = new Vector3(794, 16, 174);
		spawnPoint[2] = new Vector3(776, 14, 159);
		spawnPoint[3] = new Vector3(773, 11, 196);
		spawnPoint[4] = new Vector3(771, 18, 262);
		spawnPoint[5] = new Vector3(811, 18, 441);
		spawnPoint[6] = new Vector3(723, 24, 432);
		spawnPoint[7] = new Vector3(531, 13, 250);

		initPos = transform.position;

		fadeInOutTime = 1.5f;
		fadeImg = GameObject.Find("FadeImg").GetComponent<CanvasGroup>();

		base.Awake();
		_hitEvent = null;
		_hitEvent += () => {
			if (_stopCoroutine == null)
			{
				_stopCoroutine = StartCoroutine(PlayWakeAgain(0.2f));
			}
		};
	}

	public IEnumerator JunGIUP(ColliderCast cols, float t)
	{
		cols.End();
		// t == 총 회복량
		float time = 0;
		while(time <= 0.5f)
		{
			yield return null;
			time += Time.deltaTime;
			yy.black.Value += t * Time.deltaTime * 2;

		}
	}

	protected override IEnumerator PlayWakeAgain(float t)
	{
		GameManager.instance.DisableCtrl(false);
		yield return new WaitForSeconds(t);
		GameManager.instance.EnableCtrl();
	}

	public override void Update()
	{
		base.Update();
		if (regenOn)
		{
			GameManager.instance.uiManager.yinYangUI.RefreshValues();
		}
	}

	protected override void DamageYYBase(YinYang data)
	{
		base.DamageYYBase(data);
		GameManager.instance.uiManager.yinYangUI.RefreshValues();
	}


	public override void OnDead()
	{
		base.OnDead();
		GetActor().anim.SetDieTrigger();
		GetActor().move.moveDir = Vector3.zero;
		GetActor().move.forceDir = Vector3.zero;
		(GetActor().move as PlayerMove).ctrl.center = Vector3.up;
		(GetActor().move as PlayerMove).ctrl.height = 1;

		for (int i = 0; i < GameManager.instance.qManager.currentAbleQuest.Count; i++)
		{
			GameManager.instance.qManager.currentAbleQuest[i].ResetQuestStartTime(CompletionAct.CountSecond);
		}

		StartCoroutine(DieTel());
		

		GameManager.instance.DisableCtrl();

		Debug.Log("Player dead");
	}

	IEnumerator DieTel()
	{
		loader = GameObject.Find("TitleLoad").GetComponent<TitleLoader>();
		loader.FadeInOut("사망", 1f);
		yield return new WaitForSeconds(1f);

		StartCoroutine(FadeInOutRoutine());
		yield return new WaitForSeconds(fadeInOutTime);

		pMove = GameManager.instance.pActor.move as PlayerMove;

		//var load = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
		//yield return new WaitUntil(()=>load.isDone);

		GameManager.instance.PlayerDeath();

		if (GameManager.instance.saver.lastSave >= 0)
		{
			pMove.PlayerTeleport(spawnPoint[GameManager.instance.saver.lastSave]);
		}
		else
		{
			pMove.PlayerTeleport(initPos);
		}
		yy.black.ResetCompletely();
		yy.white.ResetCompletely();
		GameManager.instance.EnableCtrl();
	}

	IEnumerator FadeInOutRoutine()
	{
		float elapsedTime = 0f;

		// Fade In
		while (elapsedTime <= fadeInOutTime)
		{
			elapsedTime += Time.deltaTime;
			fadeImg.alpha = Mathf.Clamp01(elapsedTime / fadeInOutTime);
			yield return null;
		}

		fadeImg.alpha = 1f;

		// Wait for a moment
		yield return new WaitForSeconds(0.5f); 

		// Fade Out
		elapsedTime = fadeInOutTime; // Reset elapsed time for fade out
		while (elapsedTime >= 0f)
		{
			elapsedTime -= Time.deltaTime;
			fadeImg.alpha = Mathf.Clamp01(elapsedTime / fadeInOutTime);
			yield return null;
		}
		GetActor().Respawn();
		fadeImg.alpha = 0f;
		isFade = false;
	}
}
