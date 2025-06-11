using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    private CharacterSlot characterSlot;
    private EquipmentSlot[] equipSlots;

    // 슬롯 초기화
    public void Init()
    {
        characterSlot = GetComponentInChildren<CharacterSlot>(true);
        equipSlots = GetComponentsInChildren<EquipmentSlot>(true);

        characterSlot.Init();
        foreach (EquipmentSlot slot in equipSlots)
        {
            slot.Init();
        }
    }
}
