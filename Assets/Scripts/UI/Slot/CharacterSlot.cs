using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSlot : MonoBehaviour, IDropHandler
{
    private Player player;

    public void Init()
    {
        player = GameManager.Instance.Player;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (UIManager.dragging == null || !UIManager.dragging.Entry.item.IsUsable) return;

        player.Inventory.UseItem(UIManager.dragging.Entry, player.Stat);
    }
}
