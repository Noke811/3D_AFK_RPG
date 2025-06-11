using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    private EquipmentSlot[] slots;

    // 슬롯 초기화
    public void Init()
    {
        slots = GetComponentsInChildren<EquipmentSlot>(true);

        foreach (EquipmentSlot slot in slots)
        {
            slot.Init();
        }
    }
}
