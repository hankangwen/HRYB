using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "대화/콜백")]
public class CallbackDialogue : Dialogue
{
	//public UnityEvent evt;


	protected override IEnumerator DelShowTxt()
	{
		yield return base.DelShowTxt();
		KillerTmp();
	}

	public void KillerTmp()
	{
		GameManager.instance.KillPlayer();
	}
}
