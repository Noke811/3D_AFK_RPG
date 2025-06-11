using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public StatHandler Stat { get; private set; }
    public Inventory Inventory { get; private set; }

    [Header("EXP")]
    [SerializeField] private float baseExp = 10f;
    [SerializeField] private float expFactor = 5f;
    public float CurExp { get; private set; }
    public float NextExp { get; private set; }
    public int Level { get; private set; }

    [Header("Inventory")]
    [SerializeField] private int slotAmount = 15;
    [SerializeField] List<ItemData> datas;
    [SerializeField] List<int> amounts;

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        Stat = GetComponent<StatHandler>();
    }

    private void Start()
    {
        SetLevel(1);

        Inventory = new Inventory(slotAmount);
        GameManager.Instance.UIManager.InventoryUI.Init(slotAmount);
        GameManager.Instance.UIManager.EquipmentUI.Init();

        //test: 아이템 추가
        StartCoroutine(CoAddItem());
    }

    // 경험치 획득
    public void GetExp(float exp)
    {
        CurExp += exp;
        if (CurExp >= NextExp) LevelUp();
    }

    // 레벨 업
    public void LevelUp()
    {
        SetLevel(Level + 1);
    }

    // 레벨 설정
    private void SetLevel(int level)
    {
        Level = level;

        CurExp -= NextExp;
        NextExp = baseExp + expFactor * (Level - 1);

        Stat.ApplyLevel(Level);
    }

    // test : 아이템 1프레임 딜레이 후 추가 -> 딜레이 없이 추가하면, 프리뷰가 겹쳐서 보이는 버그 발생
    private IEnumerator CoAddItem()
    {
        for (int i = 0; i < datas.Count; i++)
        {
            Inventory.AddItem(datas[i], amounts[i]);
            yield return null;
        }
    }
}
