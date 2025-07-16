using UnityEngine;

public class CollectibleItem : MonoBehaviour, IDetectable
{
    [Header("Item Configuration")]
    public ItemSO itemData;

    public string GetDisplayName()
    {
        if (itemData == null)
        {
            Debug.LogWarning($"CollectibleItem '{name}' has no assigned ItemSO!");
            return name;
        }

        return itemData.itemName;
    }

    public void Interact()
    {
        // InventoryManager.Instance.AddItem(itemSO);
        Destroy(gameObject);
    }

    // public Sprite GetItemIcon() => itemData?.itemIcon;
    // public GameObject GetItemPrefab() => itemData?.itemPrefab3D;
}
