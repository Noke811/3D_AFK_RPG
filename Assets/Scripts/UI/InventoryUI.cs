using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform slotParent;
    [SerializeField] GameObject slotPrefab;
    private List<InventorySlot> slots;

    // 슬롯 생성 및 초기화
    public void Init(int amount)
    {
        slots = new List<InventorySlot>();

        for (int i = 0; i < amount; i++)
        {
            InventorySlot slot = Instantiate(slotPrefab, slotParent).GetComponent<InventorySlot>();
            slot.Init();
            slots.Add(slot);
        }
    }

    // 빈 슬롯에 인벤토리 아이템 연결
    public void ConnectSlot(ItemEntry entry)
    {
        foreach(InventorySlot slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.SetSlot(entry);
                return;
            }
        }
    }

    // 해당 슬롯 초기화
    public void ClearSlot(ItemEntry entry)
    {
        GetInventorySlot(entry)?.ClearSlot();
    }

    // 해당 슬롯의 수량 텍스트 업데이트
    public void UpdateSlotAmount(ItemEntry entry)
    {
        GetInventorySlot(entry)?.UpdateAmountText();
    }

    // 해당 슬롯의 장착 텍스트 업데이트
    public void UpdateSlotEquip(ItemEntry entry, bool isEquip)
    {
        GetInventorySlot(entry)?.ActiveEquipText(isEquip);
    }

    // 해당 entry가 들어간 슬롯을 반환
    public InventorySlot GetInventorySlot(ItemEntry entry)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.IsSameEntry(entry))
            {
                return slot;
            }
        }
        return null;
    }
}
