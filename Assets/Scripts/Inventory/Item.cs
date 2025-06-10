using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData data;

    public void Toched()
    {
        GameManager.Instance.Player.Inventory.AddItem(data);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameManager.TAG_PLAYER))
        {
            Toched();
        }
    }
}
