using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : BaseSlot
{
    [SerializeField] private EquipDetailType type;

    private Inventory inven;
    private StatHandler player;

    public override void Init()
    {
        inven = GameManager.Instance.Player.Inventory;
        player = GameManager.Instance.Player.Stat;

        base.Init();
    }

    public override void SetSlot(ItemEntry _entry)
    {
        base.SetSlot(_entry);

        inven.EquipItem(type, entry, player);
    }

    public override void ClearSlot()
    {
        base.ClearSlot();

        inven.UnequipItem(type, player);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (!UIManager.dragging.Entry.item.IsEquipment || !IsValidType(UIManager.dragging.Entry.item.EquipType)) return;

        base.OnDrop(eventData);
    }

    private bool IsValidType(EquipType _type)
    {
        switch (type)
        {
            case EquipDetailType.Head:
                return _type == EquipType.Head;

            case EquipDetailType.LeftHand:
            case EquipDetailType.RightHand:
                return _type == EquipType.Hand;

            default:
                return false;
        }
    }
}
