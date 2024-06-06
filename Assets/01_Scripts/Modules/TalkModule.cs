using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum DiaChangeMode
{
	Questing,
	Completed
}

public class TalkModule : Module, IInterable
{
    public Character charInfo;

	public Image qMark;

	public float yOffset = 1.5f;

	public const string QUESTCANVASNAME = "QuestMarkIndicator";


	private readonly int talkingHash = Animator.StringToHash("Talking");

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
		charInfo.latestQuest = null;
	}

	private void Start()
	{
		GameObject obj = PoolManager.GetObject(QUESTCANVASNAME, transform);
		obj.transform.localPosition = Vector3.up * yOffset;
		qMark = obj.transform.Find("QMark").GetComponent<Image>();
	}

	private void Update()
	{
		if (charInfo.Questing && charInfo.latestQuest == null) //받지않은 퀘스트 있음.
		{
			qMark.enabled = true;
			qMark.sprite = GameManager.instance.uiManager.questingMark;
		}
		else if (charInfo.latestQuest != null && charInfo.latestQuest.IsPendingCompletion) //퀘스트 완성 대기중
		{
			qMark.enabled = true;
			qMark.sprite = GameManager.instance.uiManager.questedMark;
		}
		else // ?
		{
			qMark.enabled = false;

		}
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
		self.anim.Animators.SetBool(talkingHash, true);
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
