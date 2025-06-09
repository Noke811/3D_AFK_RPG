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

[System.Serializable]
public struct StatEntry
{
    public StatType statType;
    public float value;
}

[CreateAssetMenu(fileName = "New StatData", menuName = "Data/Stat")]
public class StatData : ScriptableObject
{
    [SerializeField] private List<StatEntry> stats;

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
