using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightRangeSpin : MonoBehaviour
{
	private void LateUpdate()
	{
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, 360- GameManager.instance.camManager.pCam.m_XAxis.Value));
	}
}
