using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item")]
public class ItemSO : ScriptableObject
{
    public string itemID;
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public GameObject itemPrefab3D;
}
