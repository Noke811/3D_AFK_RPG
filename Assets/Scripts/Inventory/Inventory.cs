using System.Collections.Generic;
using UnityEngine;

public class ItemEntry
{
    public Item item;
    public int amount;
    public bool Isfull => amount >= item.MaxAmount;

    public ItemEntry(Item _item, StatHandler owner)
    {
        item = _item;
        item.ChangeOwner(owner);
        amount = 1;
    }
}

public class Inventory : MonoBehaviour
{
    private List<ItemEntry> itemList;
    private int maxAmount;

    private ItemEntry equipped = null;

    public Inventory(int _maxAmount)
    {
        itemList = new List<ItemEntry>();
        maxAmount = _maxAmount;
    }

    // 인벤토리에 아이템 추가
    public void AddItem(Item item)
    {
        // 아이템 엔트리에서 수량 추가
        foreach(ItemEntry entry in itemList)
        {
            if(entry.item.Id == item.Id && !entry.Isfull)
            {
                entry.amount++;
                return;
            }
        }

        // 새 아이템 엔트리 추가
        if(itemList.Count < maxAmount)
        {
            itemList.Add(new ItemEntry(item, GameManager.Instance.Player.Stat));
            return;
        }

        // 인벤토리가 가득 찼음
        Debug.Log("인벤토리가 가득 찼습니다.");
    }
    public void AddItem(Item item, int amount)
    {
        for(int i = 0; i < amount; i++)
            AddItem(item);
    }

    // 아이템 사용
    public void UseItem(ItemEntry entry)
    {
        entry.item.Use();

        entry.amount--;
        if(entry.amount <= 0)
            itemList.Remove(entry);
    }

    // 장비 착용
    public void EquipItem(ItemEntry entry)
    {
        if(equipped != null) equipped.item.Equip(false);

        entry.item.Equip(true);
        equipped = entry;
    }

    // 장비 해제
    public void UnequipItem(ItemEntry entry)
    {
        entry.item.Equip(false);

        if (equipped == entry)
            equipped = null;
    }
}
