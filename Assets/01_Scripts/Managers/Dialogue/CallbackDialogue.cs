using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "대화/콜백")]
public class CallbackDialogue : Dialogue
{
	//public UnityEvent evt;


	public override void NextDialogue()
	{
		KillerTmp();
		base.NextDialogue();
	}


	public void KillerTmp()
	{
		GameManager.instance.KillPlayer();
	}
}
