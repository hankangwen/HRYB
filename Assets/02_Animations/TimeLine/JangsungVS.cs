using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JangsungVS : MonoBehaviour
{
	[SerializeField] Transform tls;
	[SerializeField] Transform tls2;

    public void PlayerOnMove()
	{
		GameManager.instance.player.transform.position = tls.position;
	}
}
