using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int id;
    public int Id => id;
    [SerializeField] private StatData itemData;
    public int MaxAmount => itemData.IsUsable ? 10 : 1;

    private bool isEquip = false;

    private StatHandler owner = null;

    // 아이템 소유자 변경
    public void ChangeOwner(StatHandler _owner)
    {
        if(owner != null && itemData.IsEquipment)
        {
            Equip(false);
        }

        owner = _owner;
    }

    // 소비 아이템 사용
    public void Use()
    {
        if (owner == null || !itemData.IsUsable) return;

        // 체력 회복
        if (itemData.GetStatData().TryGetValue(StatType.Health, out float amount))
        {
            owner.Heal(amount);
        }

        // 마나 회복
        if (itemData.GetStatData().TryGetValue(StatType.Mana, out amount))
        {
            owner.RecoveryMana(amount);
        }
    }

    // 장비 아이템 착용/해제
    public void Equip(bool doEquip)
    {
        if (owner == null || !itemData.IsEquipment || isEquip == doEquip) return;

        // 공격 보너스 스탯 적용
        if (itemData.GetStatData().TryGetValue(StatType.Attack, out float atkBonus))
        {
            if(doEquip)
                owner.AddBonusStat(true, atkBonus);
            else
                owner.SubtractBonusStat(true, atkBonus);
        }

        // 방어 보너스 스탯 적용
        if (itemData.GetStatData().TryGetValue(StatType.Defense, out float defBonus))
        {
            if (doEquip)
                owner.AddBonusStat(false, defBonus);
            else
                owner.SubtractBonusStat(false, defBonus);
        }

        isEquip = doEquip;
    }
}
