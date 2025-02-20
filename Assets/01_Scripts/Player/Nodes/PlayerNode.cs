using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum BodyPart
{
	Head,
	Body,
	LArm,
	RArm,
	LLeg,
	RLeg,


	Max
}

public class PlayerNode : ScriptableObject
{
	//public int circleIndex;
	public int orderIndex;
	public BodyPart part;

	public bool completed;

	public StatUpgradeType nodeType;
	public float amt;
	public bool percentage;

	public bool learnable;

	public float needPoint;

	public UnityEvent onLearn;

	public List<PlayerNode> requirements;

	public PlayerNode()
	{
		//circleIndex = 0;
		orderIndex = 0;
		completed =false;
		amt = 0;
		requirements = new List<PlayerNode>();
		learnable = true;
		needPoint = 0;
	}

	public bool LearnNode()
	{
		if(!learnable)
			return false;

		bool res = true;

		for (int i = 0; i < requirements.Count; i++)
		{
			res &= requirements[i].completed;
		}

		if (res)
		{
			 switch (nodeType)
			 {
			 	case StatUpgradeType.Callback:
			 		onLearn?.Invoke();
			 		break;
			 	default:
					if (percentage)
					{
			 			GameManager.instance.pActor.MultStat(amt, nodeType);
					}
					else
					{
			 			GameManager.instance.pActor.AddStat(amt, nodeType);
					}
			 		break;
			 }
			
			//자원 소비시켜주기.
		}

		return res;
		
	}

	
}
