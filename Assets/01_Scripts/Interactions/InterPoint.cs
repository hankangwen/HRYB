using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InterPoint : MonoBehaviour, IInterable
{
	public bool isInterable = true;
	public float interTime = 0.3f;
	public bool altInterable = false;

	public UnityEvent onInter;
	public UnityEvent onAltInter;


	public string Name { get => transform.name; }
	public bool IsInterable { get => isInterable; set => isInterable = value; }
	public float InterTime { get => interTime; set => interTime = value; }

	public bool AltInterable => altInterable;

	public virtual InterType interType { get;set;} = InterType.Insert;
	public virtual AltInterType altInterType { get; set ; } = AltInterType.Process;

	Renderer r;
	Material mat;
	//Coroutine ongoing;

	protected virtual void Awake()
	{
		r = GetComponent<Renderer>();
		mat = r.material;
		r.material = new Material(mat);
		mat = r.material;
		GlowOff();
	}

	public void GlowOff()
	{
		//mat.SetFloat(IInterable.GlowPowerHash, 0f);
		mat.SetFloat(IInterable.GlowOnHash, 0);
	}

	public void GlowOn()
	{
		mat.SetFloat(IInterable.GlowPowerHash, 0.5f);
		mat.SetFloat(IInterable.GlowOnHash, 1);
	}

	public void InteractWith()
	{
		GameManager.instance.qManager.InvokeOnChanged(CompletionAct.InteractWith, transform.name);
		Inter();
	}

	public virtual void Inter()
	{
		onInter.Invoke();
	}

	public void AltInterWith()
	{
		AltInter();
	}

	public virtual void AltInter()
	{
		onAltInter.Invoke();
	}
}
