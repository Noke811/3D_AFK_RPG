using System.Collections.Generic;

public class ItemEntry
{
    public ItemData item;
    public int amount;
    public bool Isfull => amount >= item.MaxAmount;

    public ItemEntry(ItemData _item)
    {
        item = _item;
        amount = 1;
    }
}

public class Inventory
{
    private List<ItemEntry> itemList;
    private int maxAmount;
    private ItemEntry equipped = null;

    private InventoryUI inventoryUI;

    public Inventory(int _maxAmount)
    {
        itemList = new List<ItemEntry>();
        maxAmount = _maxAmount;
        inventoryUI = GameManager.Instance.UIManager.InventoryUI;
    }

    // 인벤토리에 아이템 추가
    public bool AddItem(ItemData item)
    {
        // 아이템 엔트리에서 수량 추가
        foreach(ItemEntry entry in itemList)
        {
            if(entry.item == item && !entry.Isfull)
            {
                entry.amount++;
                inventoryUI.UpdateSlotAmount(entry);
                return true;
            }
        }

        // 새 아이템 엔트리 추가
        if(itemList.Count < maxAmount)
        {
            ItemEntry entry = new ItemEntry(item);
            itemList.Add(entry);
            inventoryUI.ConnectSlot(entry);
            return true;
        }

        // 인벤토리가 가득 찼음
        return false;
    }
    public int AddItem(ItemData item, int amount)
    {
        int remain = amount;
        while(remain > 0)
        {
            if (!AddItem(item)) return remain;
            remain--;
        }
        
        return remain;
    }

    // 아이템 사용
    public void UseItem(ItemEntry entry, StatHandler owner)
    {
        entry.item.Use(owner);
        entry.amount--;

        if(entry.amount <= 0)
        {
            itemList.Remove(entry);
            inventoryUI.ClearSlot(entry);
        }
    }

    // 장비 착용
    public void EquipItem(ItemEntry entry, StatHandler owner)
    {
        if(equipped != null) UnequipItem(equipped, owner);

        entry.item.Equip(owner, true);
        inventoryUI.UpdateSlotEquip(entry, true);
        equipped = entry;
    }

    // 장비 해제
    public void UnequipItem(ItemEntry entry, StatHandler owner)
    {
        if (equipped == entry)
        {
            entry.item.Equip(owner, false);
            inventoryUI.UpdateSlotEquip(entry, false);
            equipped = null;
        }
    }
}
