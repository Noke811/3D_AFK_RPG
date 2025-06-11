using UnityEngine;

public class EquipmentSlot : BaseSlot
{
    [SerializeField] private EquipType type;

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
}
