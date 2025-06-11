using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PreviewGenerator previewGenerator;
    public PreviewGenerator PreviewGenerator => previewGenerator;
    [SerializeField] private GameObject TabUI;
    public InventoryUI InventoryUI { get; private set; }

    [SerializeField] Transform draggingParent;
    public Transform DraggingParent => draggingParent;
    public static BaseSlot dragging;

    private void Awake()
    {
        InventoryUI = TabUI.GetComponentInChildren<InventoryUI>(true);
    }

    private void Update()
    {   
        // Tab 키 누르면 인벤토리 활성화/비활성화
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TabUI.SetActive(!TabUI.activeSelf);
        }
    }
}
