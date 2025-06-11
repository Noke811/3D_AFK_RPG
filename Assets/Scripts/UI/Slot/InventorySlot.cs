using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : BaseSlot
{
    [Header("InventorySlot")]
    [SerializeField] private Text amountTxt;

    public bool IsSameEntry(ItemEntry _entry) => entry == _entry;

    // 슬롯 세팅
    public override void SetSlot(ItemEntry _entry)
    {
        base.SetSlot(_entry);

        amountTxt.text = entry.amount.ToString();
        amountTxt.enabled = true;
    }

    // 슬롯 초기화
    public override void ClearSlot()
    {
        base.ClearSlot();

        amountTxt.enabled = false;
    }

    // 수량 텍스트 업데이트
    public void UpdateAmountText()
    {
        if (IsEmpty) return;

        amountTxt.text = entry.amount.ToString();
    }
}
