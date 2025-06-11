using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [Header("Base Slot")]
    [SerializeField] protected Image itemImg;
    protected Vector3 startPos;
    protected Transform draggingParent;

    protected ItemEntry entry = null;
    public ItemEntry Entry => entry;
    public bool IsEmpty => entry == null;

    protected UIManager uiManager;

    // 처음 슬롯 만들어졌을 때 초기화
    public virtual void Init()
    {
        uiManager = GameManager.Instance.UIManager;
        draggingParent = GameManager.Instance.UIManager.DraggingParent;
        ClearSlot();
    }

    // 슬롯 세팅
    public virtual void SetSlot(ItemEntry _entry)
    {
        entry = _entry;

        itemImg.sprite = uiManager.PreviewGenerator.GetPreview(entry.item);
        itemImg.enabled = true;
    }

    // 슬롯 초기화
    public virtual void ClearSlot()
    {
        entry = null;

        itemImg.enabled = false;
    }

    // 드래그 시작 시
    public virtual void OnBeginDrag(PointerEventData eventData) 
    {
        startPos = itemImg.rectTransform.localPosition;

        UIManager.dragging = this;
        itemImg.GetComponent<CanvasGroup>().blocksRaycasts = false;
        itemImg.transform.SetParent(draggingParent);
    }

    // 드래그 중
    public virtual void OnDrag(PointerEventData eventData) 
    {
        itemImg.transform.position = Input.mousePosition;
    }

    // 드래그 끝
    public virtual void OnEndDrag(PointerEventData eventData) 
    {
        itemImg.GetComponent<CanvasGroup>().blocksRaycasts = true;

        itemImg.transform.SetParent(gameObject.transform);
        itemImg.rectTransform.localPosition = startPos;

        UIManager.dragging = null;
    }

    // 드랍 시
    public virtual void OnDrop(PointerEventData eventData) 
    {
        if(UIManager.dragging == this || UIManager.dragging == null)
        {
            UIManager.dragging = null;
            return;
        }

        if (!IsEmpty)
        {
            ItemEntry temp = entry;
            SetSlot(UIManager.dragging.Entry);
            UIManager.dragging.SetSlot(temp);
        }
        else
        {
            SetSlot(UIManager.dragging.Entry);
            UIManager.dragging.ClearSlot();
        }
    }
}
