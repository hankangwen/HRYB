using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Unity.VisualScripting;



/// <summary>
/// 끄고킬수있는 UI의 인터페이스 등 구조를 만들자.
/// </summary>

 public class UIManager : MonoBehaviour
{
	Canvas canvas;

	public YYCtrl yinYangUI;
	public AimPointCtrl aimUI;
	//public InfoCtrl infoUI;
	//public SimpleCrafter crafterUI;
	public QuestUICtrl questUI;
	public InterPrevUI preInterUI;
	public InterProcessUI interingUI;
    public GameObject invenPanel;
    public GameObject optionPanel;
	public DialogueUI dialogueUI;

	public NodeDetailUI detailer;

	public GameObject basicUIGroup;

	public MedicineButtonsUI medicineButton;
	public MedicineDetailUI medicineDetail;

	public ToolBarManager toolbarUIShower;

	public AnimationCurve dropDownCurve;

	public RectTransform CursorPos;
    public Image Cursor;

	public TMPro.TextMeshProUGUI debugText;

    public int DragPoint;
    public int DropPoint;

	public bool isWholeScreenUIOn
	{
		get=>isOn || isOptionOn;
	}

    bool isOn = false;
    bool isOptionOn = false;

	List<SlotUI> uis = new List<SlotUI>();
	List<QuickSlot> quickSlot = new List<QuickSlot>();

	private void Awake()
	{
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		invenPanel = canvas.transform.Find("ToolPanel").gameObject;
		optionPanel = canvas.transform.Find("OptionUI").gameObject;
		yinYangUI = canvas.GetComponentInChildren<YYCtrl>();
		aimUI = canvas.GetComponentInChildren<AimPointCtrl>();
		//infoUI = canvas.GetComponentInChildren<InfoCtrl>();
		//crafterUI = canvas.GetComponentInChildren<SimpleCrafter>();
		questUI = canvas.GetComponentInChildren<QuestUICtrl>();
		preInterUI = canvas.GetComponentInChildren<InterPrevUI>();
		interingUI = canvas.GetComponentInChildren<InterProcessUI>();
		basicUIGroup = canvas.transform.Find("Group").gameObject;
		detailer = canvas.transform.Find("ToolPanel/Node/NodeDetail").GetComponent<NodeDetailUI>();
		dialogueUI = canvas.transform.Find("DialogueUI/Dialogue").GetComponent<DialogueUI>();
		uis.AddRange(GameObject.Find("Canvas/ToolPanel/Inventory").GetComponentsInChildren<SlotUI>());

		medicineButton = canvas.transform.Find("ToolPanel/Medicine/DrugStore").GetComponent<MedicineButtonsUI>();
		medicineDetail = canvas.transform.Find("ToolPanel/Fusion/MedicineDetail").GetComponent<MedicineDetailUI>();

		toolbarUIShower = GameObject.Find("ToolPanel").GetComponent<ToolBarManager>();

		invenPanel.SetActive(true);
		optionPanel.SetActive(false);
	}

	private void Start()
	{
		
		quickSlot.AddRange(GameObject.FindObjectsOfType<QuickSlot>());
		
		interingUI.SetGaugeValue(0);
		interingUI.Off();
		preInterUI.Off();
		//infoUI.Off();
		//crafterUI.On();

		OffInven();

		
	}

	public void OnInventory(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			if (!isOn)
			{
				OnInven();
			}
			else
			{
				OffInven();
			}
		}
		
	}

	public void OnHelp(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			OnOffOption();
		}

	}

	public void UpdateInvenUI()
	{
		for (int i = 0; i < uis.Count; i++)
		{
			uis[i].UpdateItem();
		}
		toolbarUIShower.RefreshWindows();
		medicineButton.SetStatuses(GameManager.instance.pinven.CurHoldingItem.info);
		medicineDetail.RefreshInfo();
	}

	public void OnInven()
	{
		invenPanel.SetActive(true);
		isOn = true;
		GameManager.instance.UnLockCursor();
	}

	public void OffInven()
	{
		invenPanel.SetActive(false);
		isOn = false;
		GameManager.instance.LockCursor();
	}

	public void OffOption()
	{
		if (isOptionOn)
		{
			isOptionOn = false;
			optionPanel.SetActive(false);
			GameManager.instance.LockCursor();
		}		
	}

	public void OnOption()
	{
		if (!isOptionOn)
		{
			optionPanel.SetActive(true);
			GameManager.instance.UnLockCursor();
			isOptionOn = true;
		}
	}

	public void OnOffOption()
	{
		if (isOptionOn)
		{
			OffOption();
		}
		else
		{
			OnOption();
		}
	}

	public void OffCanvas()
	{
		canvas.gameObject.SetActive(false);
	}

	public void OnCanvas()
	{
		canvas.gameObject.SetActive(true);
	}
}
