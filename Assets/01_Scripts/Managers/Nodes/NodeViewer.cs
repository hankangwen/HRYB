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
	Transform lineViewTransform;

	Transform innermostNode;

	public const float RADIAN360 = Mathf.PI * 2;

	Dictionary<PlayerNode, NodeUI> nodes = new Dictionary<PlayerNode, NodeUI>();

	private void Awake()
	{
		allNodes = NodeUtility.LoadNodeData();
		nodeViewTransform = GameObject.Find("PlayerNodeViewBoard").transform;
		lineViewTransform = GameObject.Find("PlayerNodeLineViewBoard").transform;
		innermostNode = GameObject.Find("InnermostNode").transform;
	}



	public void GenerateView()
	{

		for (int i = 0; i < allNodes.Count; i++)
		{
			float angle = RADIAN360 / allNodes[i].Count;
			for (int j =0; j < allNodes[i].Count; j++)
			{
				NodeUI node = PoolManager.GetObject(baseNodeName, nodeViewTransform).GetComponent<NodeUI>();
				node.transform.localPosition = (Vector3.right * -Mathf.Sin(angle * -j) * circleRadiusScaler * (i + 1)) + (Vector3.up * Mathf.Cos(angle * -j) * circleRadiusScaler * (i + 1));
				node.SetUpNodeUI(allNodes[i][j]);
				nodes.Add(allNodes[i][j], node);
				if(allNodes[i][j].requirements.Count > 0)
				{
					for (int k = 0; k < allNodes[i][j].requirements.Count; k++)
					{
						GenerateLine(nodes[allNodes[i][j].requirements[k]].transform, node.transform);
					}
				}
				else
				{
					GenerateLine(innermostNode, node.transform);
				}
			}
		}
	}

	public void ClearView()
	{
		foreach (var item in nodes)
		{
			PoolManager.ReturnAllChilds(item.Value.gameObject);
		}
		nodes.Clear();
	}

	public void GenerateLine(Transform from, Transform to)
	{
		GameObject line = PoolManager.GetObject(baseLineName, lineViewTransform);
		Vector3 dir = to.position - from.position;
		line.transform.position = from.position;
		line.transform.localRotation = Quaternion.Euler(Vector3.forward * Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - Vector3.forward * 90);
		line.transform.localScale = Vector3.one + (Vector3.up * dir.magnitude);
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
