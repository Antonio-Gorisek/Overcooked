using UnityEngine;

[CreateAssetMenu(menuName = "Kitchen/Recipe/BurningRecipeSO")]
public class BurningRecipeSO : ScriptableObject {

    public float burningTime;
    public KitchenItemSO burnedItem;
    public KitchenItemSO cookedItem;
}
