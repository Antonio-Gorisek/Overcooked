using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Kitchen/Item/RecipeListSO")]
public class RecipeListSO : ScriptableObject
{
    public List<RecipeSO> recipeSOList;
}
