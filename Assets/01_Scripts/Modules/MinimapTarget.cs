using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MinimapRenderType
{
	Player,
	Enemy,
	EliteEnemy,

}

public class MinimapTarget : Module
{
    public bool Rendered => (GameManager.instance.player.transform.position - transform.position).sqrMagnitude <= GameManager.instance.pActor.sight.GetSightRange() * GameManager.instance.pActor.sight.GetSightRange();
	public MinimapRenderType type;

	internal Sprite myRendered;

	private void Awake()
	{
		myRendered = GameManager.instance.minimap.typeImage[((int)type)];

	}

	private void Update()
	{
		if (Rendered)
		{
			GameManager.instance.minimap.AddRender(this);
		}
	}
}
