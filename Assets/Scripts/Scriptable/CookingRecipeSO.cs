using UnityEngine;

[CreateAssetMenu(menuName = "Kitchen/Recipe/CookingRecipeSO")]
public class CookingRecipeSO : ScriptableObject {

    public float cookingTime;
    public KitchenItemSO uncookedItem;
    public KitchenItemSO cookedItem;
}

