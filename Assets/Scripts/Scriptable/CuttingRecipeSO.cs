using UnityEngine;

[CreateAssetMenu(menuName = "Kitchen/Item/CuttingRecipeSO")]
public class CuttingRecipeSO : ScriptableObject
{
    public float cuttingTime;
    public KitchenItemSO uncutItem;
    public KitchenItemSO cutItem;
}
