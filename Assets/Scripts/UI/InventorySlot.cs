using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Image itemImg;
    [SerializeField] private Text amountTxt;
    [SerializeField] private Text equipTxt;
    private Vector3 startPos;
    private Transform draggingParent;

    private ItemEntry entry = null;
    public ItemEntry Entry => entry;
    public bool IsEmpty => entry == null;
    public bool IsSameEntry(ItemEntry _entry) => entry == _entry;

    private UIManager uiManager;

    // 처음 슬롯 만들어졌을 때 초기화
    public void Init(Transform _draggingParent)
    {
        uiManager = GameManager.Instance.UIManager;
        draggingParent = _draggingParent;
        ClearSlot();
    }

    // 슬롯 세팅
    public void SetSlot(ItemEntry _entry)
    {
        entry = _entry;

        itemImg.sprite = uiManager.PreviewGenerator.GetPreview(entry.item);
        amountTxt.text = entry.amount.ToString();
        
        itemImg.enabled = true;
        amountTxt.enabled = true;
    }

    // 슬롯 초기화
    public void ClearSlot()
    {
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

    // 드래그 시작 시
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsEmpty) return;

        startPos = itemImg.rectTransform.localPosition;

        InventoryUI.dragging = this;
        itemImg.GetComponent<CanvasGroup>().blocksRaycasts = false;
        itemImg.transform.SetParent(draggingParent);
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        if (IsEmpty) return;

        itemImg.transform.position = Input.mousePosition;
    }

    // 드래그 끝
    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsEmpty) return;

        InventoryUI.dragging = null;
        itemImg.GetComponent<CanvasGroup>().blocksRaycasts = true;
        itemImg.transform.SetParent(gameObject.transform);
        itemImg.rectTransform.localPosition = startPos;
    }

    // 드랍 시
    public void OnDrop(PointerEventData eventData)
    {
         if (!IsEmpty)
        {
            ItemEntry temp = entry;
            SetSlot(InventoryUI.dragging.entry);
            InventoryUI.dragging.SetSlot(temp);
        }
        else
        {
            SetSlot(InventoryUI.dragging.entry);
            InventoryUI.dragging.ClearSlot();
        }
    }
}
