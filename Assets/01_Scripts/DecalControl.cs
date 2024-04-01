using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalControl : MonoBehaviour
{
	[ColorUsageAttribute(true, true)]
	public Color NoAimColor;
	[ColorUsageAttribute(true, true)]
	public Color AimColor;
	[ColorUsageAttribute(true, true)]
	public Color NoAimColorBack;
	[ColorUsageAttribute(true, true)]
	public Color AimColorBack;

	public Material decalMat;
	public Material auraMat;

	bool aimed = false;
	bool prevAim = false;

	private void Awake()
	{
		SetNoAimColor();
	}

	public void DetectCall(bool stat)
	{
		aimed |= stat; 
	}

	public void ResetMode()
	{
		if (aimed)
		{
			SetAimColor();
		}
		else
		{
			SetNoAimColor();
		}
		
	}

	private void LateUpdate()
	{
		if(aimed != prevAim)
		{
			prevAim = aimed;
			ResetMode();
		}
		aimed = false;
	}

	void SetAimColor()
	{
		decalMat.SetColor("_Color", AimColor);
		auraMat.SetColor("_MainColor", AimColor);
		auraMat.SetColor("_BackColor", AimColorBack);
	}
	void SetNoAimColor()
	{
		decalMat.SetColor("_Color", NoAimColor);
		auraMat.SetColor("_MainColor", NoAimColor);
		auraMat.SetColor("_BackColor", NoAimColorBack);
	}
}
