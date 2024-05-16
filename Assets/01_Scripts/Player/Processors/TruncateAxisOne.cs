using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class TruncateAxisOne : InputProcessor<Vector2>
{
#if UNITY_EDITOR
	static TruncateAxisOne()
	{
		Initialize();
	}
#endif

	[RuntimeInitializeOnLoadMethod]
	static void Initialize()
	{
		InputSystem.RegisterProcessor<TruncateAxisOne>();
	}

	public override Vector2 Process(Vector2 value, InputControl control)
	{
		if (value.x > 1)
			value.x = 1;
		else if (value.x < -1)
			value.x = -1;
		if (value.y > 1)
			value.y = 1;
		else if (value.y < -1)
			value.y = -1;
		return value;
	}
}
