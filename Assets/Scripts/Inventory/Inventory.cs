using System.Collections.Generic;

public enum EquipDetailType
{
    Head,
    LeftHand,
    RightHand,
}

// 아이템 정보와 수량을 저장
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
    private Dictionary<EquipDetailType, ItemEntry> equippedDict;

    private InventoryUI inventoryUI;

    public Inventory(int _maxAmount)
    {
        itemList = new List<ItemEntry>();
        maxAmount = _maxAmount;
        equippedDict = new Dictionary<EquipDetailType, ItemEntry>();
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
        inventoryUI.UpdateSlotAmount(entry);

        if (entry.amount <= 0)
        {
            itemList.Remove(entry);
            inventoryUI.ClearSlot(entry);
        }
    }

    // 장비 착용
    public void EquipItem(EquipDetailType type, ItemEntry entry, StatHandler owner)
    {
        if(equippedDict.ContainsKey(type)) UnequipItem(type, owner);

        equippedDict[type] = entry;
        equippedDict[type].item.Equip(owner, true);
    }

    // 장비 해제
    public void UnequipItem(EquipDetailType type, StatHandler owner)
    {
        if (equippedDict.ContainsKey(type))
        {
            equippedDict[type].item.Equip(owner, false);
            equippedDict.Remove(type);
        }
    }
}
