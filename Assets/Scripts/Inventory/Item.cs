using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData data;

    public void Toched()
    {
        if (!GameManager.Instance.Player.Inventory.AddItem(data))
            Debug.Log("인벤토리가 가득 차서 들어갈 수 없습니다!");
        else
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
