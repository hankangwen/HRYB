using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeViewer : MonoBehaviour, IOpenableWindowUI
{
    public string baseNodeName;
    public string baseLineName;

	public float circleRadiusScaler;

	List<List<PlayerNode>> allNodes;

	Transform nodeViewTransform;

	public const float RADIAN360 = Mathf.PI * 2;

	List<GameObject> nodes = new List<GameObject>();

	private void Awake()
	{
		allNodes = NodeUtility.LoadNodeData();
		nodeViewTransform = GameObject.Find("PlayerNodeViewBoard").transform;
	}



	public void GenerateView()
	{
		for (int i = 0; i < allNodes.Count; i++)
		{
			float angle = RADIAN360 / allNodes[i].Count;
			for (int j = 0; j < allNodes[i].Count; j++)
			{
				GameObject node = PoolManager.GetObject(baseNodeName, nodeViewTransform);
				node.transform.localPosition = Vector3.right * Mathf.Cos(angle * j) * circleRadiusScaler * i;
				nodes.Add(node);
				for (int k = 0; k < allNodes[i][j].requirements.Count; k++)
				{
					//선긋기.
				}
			}
		}
	}

	public void ClearView()
	{
		for (int i = 0; i < nodes.Count; i++)
		{
			PoolManager.ReturnObject(nodes[i]);
		}
		nodes.Clear();
	}

	public void GenerateLine(Transform from, Transform to)
	{
		GameObject line = PoolManager.GetObject(baseLineName, from);
		Vector3 dir = to.position - from.position;
		line.transform.rotation = Quaternion.LookRotation(dir);
		line.transform.localScale = Vector3.right * dir.magnitude;
	}

	public void OnOpen()
	{
		GenerateView();
	}

	public void WhileOpening()
	{
		
	}

	public void OnClose()
	{
		ClearView();
	}
}
