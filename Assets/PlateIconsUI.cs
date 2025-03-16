using UnityEngine;
using UnityEngine.UI;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform backgroundUI;
    [SerializeField] private Transform iconsParent;
    [SerializeField] private Transform iconPrefab;



    private void Start() {
        backgroundUI.gameObject.SetActive(false);
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        foreach (KitchenItemSO kitchenItemSO in plateKitchenObject.GetKitchenItemSOList()) {
            if (kitchenItemSO.itemName == e.kitchenItemSO.itemName) {

                backgroundUI.gameObject.SetActive(true);
                Transform iconItem = Instantiate(iconPrefab, iconsParent);
                iconItem.localPosition = Vector3.zero;
                iconItem.GetComponent<Image>().sprite = kitchenItemSO.icon;

            }
        }

    }
}
