using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Button button;
    [SerializeField] private Image itemImg;
    [SerializeField] private Text amountTxt;
    [SerializeField] private Text equipTxt;

    private ItemEntry entry = null;
    public ItemEntry Entry => entry;
    public bool IsEmpty => entry == null;
    public bool IsSameEntry(ItemEntry _entry) => entry == _entry;

    private UIManager uiManager;

    // 처음 슬롯 만들어졌을 때 초기화
    public void Init()
    {
        button = GetComponent<Button>();
        uiManager = GameManager.Instance.UIManager;

        ClearSlot();
    }

    // 슬롯 세팅
    public void SetSlot(ItemEntry _entry)
    {
        entry = _entry;

        itemImg.sprite = uiManager.PreviewGenerator.GetPreview(entry.item);
        amountTxt.text = entry.amount.ToString();
        
        button.interactable = true;
        itemImg.enabled = true;
        amountTxt.enabled = true;
    }

    // 슬롯 초기화
    public void ClearSlot()
    {
        button.interactable = false;
        itemImg.enabled = false;
        amountTxt.enabled = false;
        equipTxt.enabled = false;

        entry = null;
    }

    // 수량 텍스트 업데이트
    public void UpdateAmountText()
    {
        if (IsEmpty) return;

        amountTxt.text = entry.amount.ToString();
    }

    // 착용 상태 텍스트 활성화/비활성화
    public void ActiveEquipText(bool isEquip)
    {
        if (IsEmpty) return;

        equipTxt.enabled = isEquip;
    }
}
