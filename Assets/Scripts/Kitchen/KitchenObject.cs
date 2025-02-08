using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenItemSO kitchenItemSO;

    public KitchenItemSO GetKitchenItem() {
        return kitchenItemSO;
    }
}
