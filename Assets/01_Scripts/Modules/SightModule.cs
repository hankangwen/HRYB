using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightModule : Module
{
	public float initSightRange = 10;

	public UpgradableStatus sightRange;

	public virtual void Awake()
	{
		sightRange = new UpgradableStatus(2, initSightRange);
	}

	public float GetSightRange()
	{
		return sightRange.MaxValue;
	}

	public override void ResetStatus()
	{
		base.ResetStatus();
		sightRange.ResetCompletely();
	}
}
