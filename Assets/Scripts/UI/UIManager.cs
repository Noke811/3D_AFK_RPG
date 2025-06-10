using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PreviewGenerator previewGenerator;
    public PreviewGenerator PreviewGenerator => previewGenerator;
    [SerializeField] private GameObject TabUI;
    public InventoryUI InventoryUI { get; private set; }

    private void Awake()
    {
        InventoryUI = TabUI.GetComponentInChildren<InventoryUI>(true);
    }

    private void Update()
    {   
        // test
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TabUI.SetActive(!TabUI.activeSelf);
        }
    }
}
