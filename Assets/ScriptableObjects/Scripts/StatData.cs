using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Health,
    Mana,
    Attack,
    Defense,
    AttackGrowth,
    DefenseGrowth,
}

public enum StatDataType
{
    Character,
    Usable,
    Equipment,
}

[System.Serializable]
public class StatEntry
{
    public StatType statType;
    public float value;
}

[CreateAssetMenu(fileName = "New StatData", menuName = "Data/Stat")]
public class StatData : ScriptableObject
{
    [SerializeField] private StatDataType type;
    [SerializeField] private List<StatEntry> stats;

    public bool IsCharacter => type == StatDataType.Character;
    public bool IsUsable => type == StatDataType.Usable;
    public bool IsEquipment => type == StatDataType.Equipment;

    // 스탯 데이터에 대한 딕셔너리 반환
    public Dictionary<StatType, float> GetStatData()
    {
        Dictionary<StatType, float> dict = new();
        foreach (var entry in stats)
        {
            dict[entry.statType] = entry.value;
        }
        return dict;
    }
}
