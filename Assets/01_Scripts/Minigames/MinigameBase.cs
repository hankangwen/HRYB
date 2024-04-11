using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameBase : MonoBehaviour
{
	public string feedbackerName;
	public string minigameSceneName;

	public Minigames myMode;

	protected ItemAmountPair minigameTarget;

	GameObject minigameZone;

	Animator feedbacks;
	private readonly int ActHash = Animator.StringToHash("Act");
	
	public virtual void Awake()
	{
		if(minigameSceneName == ""){
			minigameSceneName = MinigameManager.GetSceneName(myMode);
		}
		minigameZone = GameObject.Find(minigameSceneName);
		feedbacks = GameObject.Find(feedbackerName).GetComponent<Animator>();
	}

	public virtual void StartGame(ItemAmountPair objName)
	{
		minigameTarget = objName;
		minigameZone.SetActive(true);
	}

	public virtual void EndGame()
	{
		Debug.Log($"아이템 : {minigameTarget.info.MyName}에 대한 미니게임 성공.");
		minigameZone.SetActive(false);

		MinigameManager.UnloadMinigame();
	}

	public virtual void FailGame()
	{

		Debug.Log($"아이템 : {minigameTarget.info.MyName}에 대한 미니게임 실패.");
		minigameZone.SetActive(false);

		MinigameManager.UnloadMinigame();
	}

	public virtual bool DoGameCheck()
	{
		return false;
	}

	public virtual void ShowFeedback()
	{
		feedbacks.SetTrigger(ActHash);
	}
}
