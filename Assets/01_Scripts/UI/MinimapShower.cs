using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapShower : MonoBehaviour
{
	RawImage img;

    public void SetInfo(Texture tx)
	{
		if(img == null)
			img = GetComponent<RawImage>();

		img.texture = tx;
	}
}
