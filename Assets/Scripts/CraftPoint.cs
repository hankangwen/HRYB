using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CraftPoint : InterPoint
{
	public HashSet<YinyangItem> holding = new HashSet<YinyangItem>();
	public HashSet<YinyangItem> result = new HashSet<YinyangItem>();
	Stack<YinyangItem> insertOrder = new Stack<YinyangItem>();

	public new UnityEvent onInter;
	public UnityEvent onAlternative;

	protected bool processing = false;

	private void Awake()
	{
		onInter.AddListener( NormalInter);
		onAltInter.AddListener(AltInter);
	}

	public virtual void NormalInter() //������ �ֱ�, ���μ��� ����
	{
		if(GameManager.instance.pinven.curHolding == -1 || GameManager.instance.pinven.inven[GameManager.instance.pinven.curHolding].isEmpty())
		{
			Process();
		}
		else
		{
			YinyangItem info = Item.nameDataHashT[GameManager.instance.pinven.curHolding] as YinyangItem;
			if (info != null)
			{
				holding.Add(info);
				insertOrder.Push(info);
				Debug.Log($"ADDED {info.myName}");
			}
		}
		
	}

	public virtual void AltInter() //������ ������ �Ǵ� (���μ��� ���� (�� ����� ��쿡���� �ִ�.))
	{
		YinyangItem top = insertOrder.Pop();
		if (GameManager.instance.pinven.AddItem(top) == 0)
		{
			holding.Remove(top);
			Debug.Log($"Removed {top.myName}");
		}

	}

	public virtual void Process()
	{
		processing = true;
	}
}
