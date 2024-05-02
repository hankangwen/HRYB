using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum DiaChangeMode
{
	Questing,
	Completed
}

public class TalkModule : Module, IInterable
{
    public Character charInfo;


	public string Name => charInfo.baseName;

	public bool IsInterable { get;set; }

	public bool AltInterable { get;set;} = false;

	public float InterTime { get; set; } = 0;
	public InterType interType { get; set; } = InterType.Talk;
	public AltInterType altInterType { get; set; }

	public UnityEvent onNextTalk;

	private void Awake()
	{
		charInfo.self = GetActor();
	}

	public void AltInter()
	{
		
	}

	public void AltInterWith()
	{
		
	}

	public void GlowOff()
	{
		
	}

	public void GlowOn()
	{
		//사람이 빛나진 않지않을까..
	}

	public void Inter()
	{
		GameManager.instance.qManager.InvokeOnChanged(CompletionAct.InteractWith, transform.name);
		charInfo.OnTalk();
	}

	public void InteractWith()
	{
		Inter();
	}

	public void SetDialogue(Dialogue dia)
	{
		charInfo.SetDialogue(dia);
	}

	public override void ResetStatus()
	{
		base.ResetStatus();
		charInfo.ResetDialogue();
	}
}
