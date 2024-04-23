using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class NormalizeIfOverOne : InputProcessor<Vector2>
{
#if UNITY_EDITOR
	static NormalizeIfOverOne()
	{
		Initialize();
	}
#endif

	[RuntimeInitializeOnLoadMethod]
	static void Initialize()
	{
		InputSystem.RegisterProcessor<NormalizeIfOverOne>();
	}

	public override Vector2 Process(Vector2 value, InputControl control)
	{
		if(value.sqrMagnitude > 1)
			return value.normalized;
		else
			return value;
	}
}
