using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IgnoreTransparent : MonoBehaviour
{
	Image img;
	private void Awake()
	{
		img = GetComponent<Image>();
		if(img)
			img.alphaHitTestMinimumThreshold = 0.5f;
	}
}
