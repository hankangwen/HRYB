using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreservedDataManager : MonoBehaviour
{
	public int lastSave;

	private void Awake()
	{
		DontDestroyOnLoad(this);
	}
}
