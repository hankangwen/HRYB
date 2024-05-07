using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrogeNode : INode
{
	Actor self;
	EnemyLifeModule _enemy;

	public GrogeNode(Actor self)
	{
		this.self = self;
		_enemy = self.GetComponent<EnemyLifeModule>();
	}
	public NodeStatus Examine()
	{
		if (_enemy.IsGroge)
		{
			// 에니메이션 추가필요
			Debug.Log("그로기 상태 에니메이션 없음");

			self.anim.SetBoolModify("Stun", true);
			return NodeStatus.Run;
		}
		else if (self.life._bufferDurations.ContainsKey(StatEffID.Stun))
		{
			
			if (self.life._bufferDurations[StatEffID.Stun] < 0)
			{
				self.anim.SetBoolModify("Stun", false);

			}
		}



		return NodeStatus.Fail;
	}

}
