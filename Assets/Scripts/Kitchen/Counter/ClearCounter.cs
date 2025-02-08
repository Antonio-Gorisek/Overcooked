using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform counerSpawnPoint;
    [SerializeField] private KitchenItemSO kitchenItemSO;


    public void Interact() {
        if (kitchenItemSO == null || counerSpawnPoint == null)
            return;

        Transform kitchenObject = Instantiate(kitchenItemSO.itemPrefb, counerSpawnPoint);
        Debug.Log(kitchenObject.GetComponent<KitchenObject>().GetKitchenItem().itemName);
    }
}
