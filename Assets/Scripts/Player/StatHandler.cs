using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [SerializeField] private StatData data;
    private Dictionary<StatType, float> statDict;

    [Header("EXP")]
    [SerializeField] private float baseExp = 10f;
    [SerializeField] private float expFactor = 5f;

    public int Level { get; private set; }
    public float CurHp { get; private set; }
    public float CurMp { get; private set; }
    public float CurExp { get; private set; }
    public float NextExp { get; private set; }

    private float baseAttack;
    private float bonusAttack = 0f;
    public float Attack => baseAttack + bonusAttack;

    private float baseDefense;
    private float bonusDefense = 0f;
    public float Defense => baseDefense + bonusDefense;

    // 캐릭터 스탯 초기화
    public void Init(int _level = 1)
    {
        statDict = data.GetStatData();

        SetLevel(_level);
    }

    // 레벨 설정
    private void SetLevel(int _level)
    {
        // 레벨업 시 체력, 마나 회복
        CurHp = statDict[StatType.Health];
        CurMp = statDict[StatType.Mana];

        Level = _level;
        int lvMinusOne = Level - 1;

        CurExp = 0f;
        NextExp = baseExp + expFactor * lvMinusOne;
        baseAttack = statDict[StatType.Attack] + statDict[StatType.AttackGrowth] * lvMinusOne;
        baseDefense = statDict[StatType.Defense] + statDict[StatType.DefenseGrowth] * lvMinusOne;
    }

    // 레벨 업
    public void LevelUp()
    {
        SetLevel(Level + 1);
    }

    // 데미지
    public void TakeDamage(float damage)
    {
        if (damage < 0)
        {
            Debug.Log("올바르지 않은 데미지가 들어왔습니다.");
            return;
        }

        float realDamage = damage - Defense;
        if (realDamage <= 0.1f) realDamage = 0.1f;

        CurHp -= realDamage;
        if (CurHp < 0f)
        {
            CurHp = 0f;
            // 사망 상태로
        }
    }

    // 보너스 스탯 설정
    public void SetBonusStat(bool isAttackStat, float bonus)
    {
        if (isAttackStat)
        {
            bonusAttack = bonus;
        }
        else
        {
            bonusDefense = bonus;
        }
    }
    
    // 보너스 스탯 초기화
    public void ResetBonusStat(bool isAttackStat)
    {
        if (isAttackStat)
        {
            bonusAttack = 0f;
        }
        else
        {
            bonusDefense = 0f;
        }
    }
}
