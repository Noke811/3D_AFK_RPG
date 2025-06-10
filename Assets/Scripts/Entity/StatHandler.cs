using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [SerializeField] private StatData data;
    private Dictionary<StatType, float> statDict;

    public float CurHp { get; private set; }
    public float MaxHp => statDict?.GetValueOrDefault(StatType.Health) ?? 0f;
    public float CurMp { get; private set; }
    public float MaxMp => statDict?.GetValueOrDefault(StatType.Mana) ?? 0f;

    private float baseAttack;
    private float bonusAttack = 0f;
    public float Attack => baseAttack + bonusAttack;

    private float baseDefense;
    private float bonusDefense = 0f;
    public float Defense => baseDefense + bonusDefense;

    private void Awake()
    {
        statDict = data.IsCharacter ? data.GetStatData() : null;
        if(statDict == null)
        {
            Debug.Log("잘못된 캐릭터 스탯이 들어갔습니다.");
        }
    }

    // 레벨에 따른 스탯 증가 및 회복
    public void ApplyLevel(int level)
    {
        baseAttack = statDict[StatType.Attack] + statDict[StatType.AttackGrowth] * (level - 1);
        baseDefense = statDict[StatType.Defense] + statDict[StatType.DefenseGrowth] * (level - 1);

        // 레벨업 시 체력, 마나 회복
        Heal(MaxHp);
        RecoveryMana(MaxMp);
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
            Debug.Log("플레이어 사망!");
            // 사망 상태로
        }
    }

    // 체력 회복
    public void Heal(float amount)
    {
        CurHp += amount;
        if(CurHp > MaxHp) CurHp = MaxHp;
    }

    // 마나 회복
    public void RecoveryMana(float amount)
    {
        CurMp += amount;
        if(CurMp > MaxMp) CurMp = MaxMp;
    }

    // 보너스 스탯 추가
    public void AddBonusStat(bool isAttackStat, float bonus)
    {
        if (isAttackStat)
        {
            bonusAttack += bonus;
        }
        else
        {
            bonusDefense += bonus;
        }
    }

    // 보너스 스탯 감소
    public void SubtractBonusStat(bool isAttackStat, float bonus)
    {
        if (isAttackStat)
        {
            bonusAttack -= bonus;
        }
        else
        {
            bonusDefense -= bonus;
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
