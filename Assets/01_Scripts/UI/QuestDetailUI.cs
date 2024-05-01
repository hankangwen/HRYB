using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDetailUI : MonoBehaviour
{
	QuestInfo curShown;

	public void ShowInfoOf(QuestInfo inf)
	{
		if(inf == null)
		{
			OffCompletely();
			return;
		}
		curShown = inf;
		On();

		for (int i = 0; i < inf.myInfo.Count; i++)
		{
			if (inf.myInfo[i].isVisibleCondition)
			{


			}
		}
	}

	public void RefreshCompInfo()
	{

	}

	public void OffCompletely()
	{
		gameObject.SetActive(false);
	}

	public void On()
	{
		gameObject.SetActive(true);
	}
}
