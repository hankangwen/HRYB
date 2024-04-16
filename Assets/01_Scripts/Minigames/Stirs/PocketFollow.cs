using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketFollow : MonoBehaviour
{
	Transform ball;
	Camera stirCam;

	Ray r;
	RaycastHit hit;

	private void Awake()
	{
		ball = GameObject.Find("StirBall").transform;
		stirCam = GameObject.Find("StirCam").GetComponent<Camera>();
	}

	void FixedUpdate()
    {
		r = stirCam.ScreenPointToRay(ball.position);
		if (Physics.Raycast(r, out hit))
		{
			transform.position = hit.point;
		}
    }
}
