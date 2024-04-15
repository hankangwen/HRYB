using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;




public class PlayerNode : ScriptableObject
{
	public int circleIndex;
	public int orderIndex;

	public bool completed;

	public bool isSpecialNode;

	public StatUpgradeType nodeType;
	public float amt;

	public UnityEvent onLearn;

	public List<PlayerNode> requirements;

	public PlayerNode()
	{
		circleIndex = 0;
		orderIndex = 0;
		completed =false;
		isSpecialNode = false;
		amt = 0;
		requirements = new List<PlayerNode>();
	}

	public bool LearnNode()
	{
		bool res = true;

		for (int i = 0; i < requirements.Count; i++)
		{
			res &= requirements[i].completed;
		}

		if (res)
		{
			if (isSpecialNode)
			{
				onLearn?.Invoke();
			}
			else
			{
				switch (nodeType) //@@@@@@@@스탯올려주기.
				{
					case StatUpgradeType.White:
						break;
					case StatUpgradeType.Black:
						break;
					case StatUpgradeType.Atk:
						break;
					case StatUpgradeType.MoveSpeed:
						break;
					case StatUpgradeType.CooldownRdc:
						break;
					default:
						break;
				}
			}
		}

		return res;
		
	}
}
