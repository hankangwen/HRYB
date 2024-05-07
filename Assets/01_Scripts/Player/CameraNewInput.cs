using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNewInput : CinemachineInputProvider
{
	[Header("Value")]
	[Range(0f, 1f)] [SerializeField] public float XAxisDamping = 1f;
	[Range(0f, 1f)] [SerializeField] public float YAxisDamping = 1f;

	[Header("CamSenstive")]
	[SerializeField] public float Second = 0.2f;


	float x_curtime = 0;
	float y_curtime = 0;
	bool _camMove = false;



	public override float GetAxisValue(int axis)
	{
		if (x_curtime >= Second || y_curtime >= Second)
		{
			_camMove = true;
		}

		

		//Debug.LogError($"Cam Value : {x_curtime}");

		if (enabled)
		{
			var action = ResolveForPlayer(axis, axis == 2 ? ZAxis : XYAxis);

			

			if((int)(action.ReadValue<Vector2>().x) != 0)
			{
				x_curtime += Time.deltaTime;
			}
			else
			{
				x_curtime = 0;
			}

			if ((int)(action.ReadValue<Vector2>().y) != 0)
			{
				y_curtime += Time.deltaTime;
			}
			else
			{
				y_curtime = 0;
			}


			if (x_curtime == 0f && y_curtime == 0f)
			{
				//Debug.LogError($"Cam Value : Reset");
				_camMove = false;
				return 0;
			}

			if (action != null && _camMove)
			{
				//Debug.LogError($"Cam Value : {action.ReadValue<Vector2>().x}, {action.ReadValue<Vector2>().y}");
				switch (axis)
				{
					case 0: return action.ReadValue<Vector2>().x * XAxisDamping;
					case 1: return action.ReadValue<Vector2>().y * YAxisDamping;
					case 2: return action.ReadValue<float>();
				}
			}



		}

		return 0;
	}
}
