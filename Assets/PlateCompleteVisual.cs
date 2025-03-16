using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenGameObject {
        public KitchenItemSO kitchenItemSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenGameObject> kitchenObjectSO;

    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        foreach (KitchenGameObject kithcnGameObject in kitchenObjectSO) {
            kithcnGameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        foreach(KitchenGameObject kithcnGameObject in kitchenObjectSO) {
            if(kithcnGameObject.kitchenItemSO == e.kitchenItemSO) {
                kithcnGameObject.gameObject.SetActive(true);
            }
        }
    }
}
