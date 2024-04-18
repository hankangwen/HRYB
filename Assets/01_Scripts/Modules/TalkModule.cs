using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkModule : Module, IInterable
{
    public Character charInfo;

	Dialogue defaultDia;

	public string Name => charInfo.baseName;

	public bool IsInterable { get;set; }

	public bool AltInterable { get;set;} = false;

	public float InterTime { get; set; } = 0;
	public InterType interType { get; set; } = InterType.Talk;
	public AltInterType altInterType { get; set; }

	private void Awake()
	{
		defaultDia = charInfo.talkData;
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
		charInfo.OnTalk();
	}

	public void InteractWith()
	{
		Inter();
	}

	public void SetDialogue(Dialogue dia)
	{
		charInfo.talkData = dia;
	}

	public override void ResetStatus()
	{
		base.ResetStatus();
		charInfo.talkData = defaultDia;
	}
}
