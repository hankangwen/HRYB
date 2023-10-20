using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUIController : MonoBehaviour
{
    public List<InventorySlot> InventoryItems = new List<InventorySlot>();

    private VisualElement m_Root;
    private VisualElement m_SlotContainer;

    private void Awake()
    {
        //��Ʈ�� UI������Ʈ ����
        m_Root = GetComponent<UIDocument>().rootVisualElement;

        //��Ʈ���� SlotContainer ã��
        m_SlotContainer = m_Root.Query("SlotContainer");

        //�κ� ���� ��,  SlotContainer�� �ڽ����� �߰�
        for (int i = 0; i < 20; i++)
        {
            InventorySlot item = new InventorySlot();

            InventoryItems.Add(item);

            m_SlotContainer.Add(item);
        }
    }
}
