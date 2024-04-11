using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HumenNormalAttack : AttackBase
{


	int posValue2 = -1;
	List<YGArrow> _arrow = new();

	public override void UpdateStatus()
	{
		//throw new System.NotImplementedException();
	}

	internal override void MyDisoperation(Actor self)
	{
		//throw new System.NotImplementedException();
	}

	internal override void MyOperation(Actor self)
	{
		//throw new System.NotImplementedException();
	}

	public override void OnAnimationStart(Actor self, AnimationEvent evt)
	{
		GameManager.instance.DisableCtrl(false);
		_arrow.Clear();
		posValue2 = -2;
	}

	public override void OnAnimationEvent(Actor self, AnimationEvent evt)
	{


		switch(evt.intParameter)
		{
			case 0:
				{
					GameObject obj = PoolManager.GetObject("HumenBullet", self.transform);
					if(obj.TryGetComponent<YGArrow>(out YGArrow _yg))
					{
						_yg.Ready(self, self.transform.position + self.transform.forward.normalized * 1,
						(_pos, enemy, time) =>
						{
							obj.transform.position = Vector3.Lerp(_pos, enemy, time / 0.7f);
						},
						(_life) =>
						{
							DoDamage(_life.GetActor(), self, obj.transform.position);
						},
						(_trm, _life) =>
						{
							// null
						});

						_yg.Fire();
					}	
				}
				break;
			case 1:
				{
					if (evt.stringParameter == "Fire")
					{
						_arrow.ForEach((g) => { g.Fire(); });
						_arrow.Clear();
					}
					else
					{
						GameObject obj = PoolManager.GetObject("HumenBullet", self.transform);
						if (obj.TryGetComponent<YGArrow>(out YGArrow _yg))
						{
							int f = posValue2 > 0 || posValue2 == -1 ? 0 : 1;

							_yg.Ready(self, self.transform.position + self.transform.forward.normalized * f + self.transform.right * posValue2,
							(_pos, enemy, time) =>
							{
								obj.transform.position = Vector3.Lerp(_pos, enemy, time / 0.7f);
							},
							(_life) =>
							{
								DoDamage(_life.GetActor(), self, 0.4f, obj.transform.position);
							},
							(_trm, _life) =>
							{
								// null
							});
							posValue2++;
							_arrow.Add(_yg);
						}
					}
				}
				break;
			case 2:
				{
					GameObject obj = PoolManager.GetObject("HumenNormalUnder", self.transform);
					if (obj.TryGetComponent<ColliderCast>(out ColliderCast _cols))
					{
						_cols.Now(self.transform, (_life) =>
						{
							
						});
					}
				}
				break;
			case 3:
				{

				}
				break;
		}
	}

	public override void OnAnimationMove(Actor self, AnimationEvent evt)
	{

	}

	public override void OnAnimationEnd(Actor self, AnimationEvent evt)
	{
		
	}

	public override void OnAnimationStop(Actor self, AnimationEvent evt)
	{
		GameManager.instance.EnableCtrl();

	}

	public override void OnAnimationHit(Actor self, AnimationEvent evt)
	{
		_arrow.ForEach((g) => { g.Fire(); });
		_arrow.Clear();
	}
}
