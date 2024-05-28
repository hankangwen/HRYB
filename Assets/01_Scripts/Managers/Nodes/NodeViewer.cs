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

	GameObject nodeSelection;
	List<GameObject> partedNode;

	public NodeLearnUI nodeLearner;
	Transform viewport;

	Transform innermostNode;

	public const float RADIAN360 = Mathf.PI * 2;
	public const float VIEWPORTOFFSET = 300;
	public const float CIRCLESEC = 0.5f;
	public const float MOVESEC = 0.75f;

	Dictionary<PlayerNode, NodeUI> nodes = new Dictionary<PlayerNode, NodeUI>();

	private void Awake()
	{
		//allNodes = NodeUtility.LoadNodeData();
		//nodeViewTransform = GameObject.Find("PlayerNodeNodeViewBoard").transform;
		//lineViewTransform = GameObject.Find("PlayerNodeLineViewBoard").transform;
		//innermostNode = GameObject.Find("InnermostNode").transform;
		nodeSelection = GameObject.Find("BodyPartSelection").gameObject;
		partedNode = new List<GameObject>();
		for (int i = 0; i < (int)BodyPart.Max; i++)
		{
			partedNode.Add(GameObject.Find($"{((BodyPart)i).ToString()}Bgnd"));

		}
		nodeLearner = transform.Find("NodeLearnWindow").GetComponent<NodeLearnUI>();
		viewport = transform.Find("NodeBgnd/Viewport");
	}

	private void Start()
	{
		
		ResetView();
	}


	public void ResetView()
	{
		nodeSelection.SetActive(true);
		for (int i = 0; i < partedNode.Count; i++)
		{
			partedNode[i].SetActive(false);
		}
	}
	//public void GenerateView()
	//{

	//	for (int i = 0; i < allNodes.Count; i++)
	//	{
	//		float angle = RADIAN360 / allNodes[i].Count;
	//		for (int j =0; j < allNodes[i].Count; j++)
	//		{
	//			NodeUI node = PoolManager.GetObject(baseNodeName, nodeViewTransform).GetComponent<NodeUI>();
	//			node.transform.localPosition = (Vector3.right * -Mathf.Sin(angle * -j) * circleRadiusScaler * (i + 1)) + (Vector3.up * Mathf.Cos(angle * -j) * circleRadiusScaler * (i + 1));
	//			node.SetUpNodeUI(allNodes[i][j]);
	//			nodes.Add(allNodes[i][j], node);
	//			if(allNodes[i][j].requirements.Count > 0)
	//			{
	//				for (int k = 0; k < allNodes[i][j].requirements.Count; k++)
	//				{
	//					GenerateLine(nodes[allNodes[i][j].requirements[k]].transform, node.transform);
	//				}
	//			}
	//			else
	//			{
	//				GenerateLine(innermostNode, node.transform);
	//			}
	//		}
	//	}
	//}

	public void ClearView()
	{
		foreach (var item in nodes)
		{
			PoolManager.ReturnAllChilds(item.Value.gameObject);
		}
		nodes.Clear();
	}

	public void ShowLearner(PlayerNode node)
	{
		nodeLearner.On(node);
		StartCoroutine(DelMoveViewport(true));
	}

	public void UnshowLearner()
	{
		nodeLearner.Off();
		StartCoroutine(DelMoveViewport(false));

	}

	IEnumerator DelMoveViewport(bool direction)
	{
		float t = 0;
		Vector3 originalPos = viewport.position;
		float accOffset = 0;
		while(t < MOVESEC)
		{
			yield return null;
			t += Time.deltaTime;
			accOffset = Mathf.Lerp(0, VIEWPORTOFFSET, t / MOVESEC);
			viewport.position = originalPos + (direction ? Vector3.right : Vector3.left) * accOffset;

		}
		viewport.position = originalPos + (direction ? Vector3.right : Vector3.left) * VIEWPORTOFFSET;
	}

	//public void GenerateLine(Transform from, Transform to)
	//{
	//	GameObject line = PoolManager.GetObject(baseLineName, lineViewTransform);
	//	Vector3 dir = to.position - from.position;
	//	line.transform.position = from.position;
	//	line.transform.localRotation = Quaternion.Euler(Vector3.forward * Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - Vector3.forward * 90);
	//	line.transform.localScale = Vector3.one + (Vector3.up * dir.magnitude);
	//}

	public void OnOpen()
	{
		//GenerateView();
		ResetView();
	}

	public void WhileOpening()
	{
		
	}

	public void OnClose()
	{
		//ClearView();
	}

	public void Refresh()
	{

	}
}
