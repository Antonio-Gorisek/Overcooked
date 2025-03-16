using UnityEngine;

[CreateAssetMenu(menuName = "Kitchen/Item/KitchenItemSO")]
public class KitchenItemSO : ScriptableObject
{
    public Transform itemPrefb;
    public Sprite icon;
    public string itemName;
}
