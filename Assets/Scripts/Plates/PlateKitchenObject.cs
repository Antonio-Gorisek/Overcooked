using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenItemSO kitchenItemSO;
    }

    [SerializeField] private List<KitchenItemSO> validKitchenObjects = new List<KitchenItemSO>(); 
    private List<KitchenItemSO> kitchenObjectSOList = new List<KitchenItemSO>();

    public List<KitchenItemSO> GetKitchenItemSOList() {
        return kitchenObjectSOList;
    }

    public bool TryAddIngredient(KitchenItemSO kitchenObjectSO) {
        if(!validKitchenObjects.Contains(kitchenObjectSO)) {
            return false;
        }

        if(kitchenObjectSOList.Contains(kitchenObjectSO)) {
            return false;
        } else {
            kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
                kitchenItemSO = kitchenObjectSO
            });

            return true;
        }

    }
}
