using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Kitchen/Recipe/NewRecipeSO")]
public class RecipeSO : ScriptableObject
{
    public List<KitchenItemSO> kitchenItemSoList;
    public string recipeName;
}
