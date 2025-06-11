using UnityEngine;

public enum EquipType
{
    Head,
    Hand,
}

[CreateAssetMenu(fileName = "New Item", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private StatData statData;
    public int MaxAmount => statData.IsUsable ? 10 : 1;
    public bool IsUsable => statData.IsUsable;
    public bool IsEquipment => statData.IsEquipment;

    // 장비만 설정
    [SerializeField] private EquipType equipType;
    public EquipType EquipType => equipType;

    [SerializeField] private GameObject itemPrefab;
    public GameObject ItemPrefab => itemPrefab;

    // 소비 아이템 사용
    public void Use(StatHandler owner)
    {
        if (owner == null || !IsUsable) return;

        if (statData.GetStatData().TryGetValue(StatType.Health, out float hpAmount))
            owner.Heal(hpAmount);

        if (statData.GetStatData().TryGetValue(StatType.Mana, out float mpAmount))
            owner.RecoveryMana(mpAmount);
    }

    // 장비 아이템 착용/해제
    public void Equip(StatHandler owner, bool doEquip)
    {
        if (owner == null || !IsEquipment) return;

        if (statData.GetStatData().TryGetValue(StatType.Attack, out float atk))
        {
            if (doEquip) owner.AddBonusStat(true, atk);
            else owner.SubtractBonusStat(true, atk);
        }

        if (statData.GetStatData().TryGetValue(StatType.Defense, out float def))
        {
            if (doEquip) owner.AddBonusStat(false, def);
            else owner.SubtractBonusStat(false, def);
        }
    }
}
