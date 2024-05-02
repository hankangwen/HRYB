using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOpenableWindowUI
{
    public void OnOpen();
	public void WhileOpening();
	public void OnClose();

	public void Refresh();
}
