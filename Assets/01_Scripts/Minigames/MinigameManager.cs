using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Minigames
{
	None,
	Cut,

}
public class MinigameManager
{
	public const string MINIGAMENAME = "Minigame";

	static Minigames curMinigame = Minigames.None;

    public static void LoadMinigame(Minigames mode)
	{
		if (curMinigame == Minigames.None)
		{
			SceneManager.LoadSceneAsync(GetSceneName(mode));
			Time.timeScale = 0;
			GameManager.instance.pinp.DeactivateInput();
			curMinigame = mode;
		}
	}

	public static void UnloadMinigame()
	{
		if (curMinigame != Minigames.None)
		{
			SceneManager.UnloadSceneAsync(GetSceneName(curMinigame));
			Time.timeScale = 1;
			GameManager.instance.pinp.ActivateInput();
		}
	}

	public static string GetSceneName(Minigames mode)
	{
		switch (mode)
		{
			case Minigames.Cut:
				return $"Cut{MINIGAMENAME}";
			default:
				throw new UnityException($"{mode} : 해당 모드는 존재하지 않음.");
		}
	}
}
