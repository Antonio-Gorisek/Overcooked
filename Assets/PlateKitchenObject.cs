using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenItemSO> validKitchenObjects = new List<KitchenItemSO>(); 
    private List<KitchenItemSO> kitchenObjectSOList = new List<KitchenItemSO>();


    public bool TryAddIngredient(KitchenItemSO kitchenObjectSO) {
        if(!validKitchenObjects.Contains(kitchenObjectSO)) {
            return false;
        }

        if(kitchenObjectSOList.Contains(kitchenObjectSO)) {
            return false;
        } else {
            kitchenObjectSOList.Add(kitchenObjectSO);
            return true;
        }

    }
}
