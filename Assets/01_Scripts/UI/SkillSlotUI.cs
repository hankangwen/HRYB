using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
	public SkillSlotInfo slot;
	PlayerCast pCast;

	float curCool;
	float maxCool;

	Image coolDown;
	Image skillIcon;

	public Sprite lockImg;

	private void Awake()
	{
		skillIcon = transform.GetChild(0).GetComponent<Image>();
		coolDown = transform.GetChild(1).GetComponent<Image>();
	}

	private void Start()
	{
		pCast = GameManager.instance.pActor.cast as PlayerCast;
	}


	public void setCooldown()
	{
		if (pCast.nowSkillSlot[(int)slot].skInfo != null)
		{
			skillIcon.sprite = pCast.nowSkillSlot[(int)slot].skInfo.skillIcon;
		}
		else
		{
			skillIcon.sprite = lockImg;
		}

		if (pCast.nowSkillSlot[(int)slot].skInfo != null)
		{
			curCool = 1 - pCast.nowSkillSlot[(int)slot].CurCooledTime / pCast.nowSkillSlot[(int)slot].skInfo.cooldown;
		}
	}

	private void Update()
	{

		setCooldown();
		coolDown.fillAmount = curCool;
	}

}
