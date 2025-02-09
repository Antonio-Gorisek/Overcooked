using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenItemSO kitchenItemSO;
    private IKitchenObject kitchenObject;

    public KitchenItemSO GetKitchenItem() {
        return kitchenItemSO;
    } 
    
    public IKitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void SetKitchenObject(IKitchenObject kitchenObject) {
        if(this.kitchenObject != null) {
            this.kitchenObject.ClearKitchenObject();
        }

        this.kitchenObject = kitchenObject;
        kitchenObject.SetKitchenObject(this);

        transform.position = kitchenObject.GetKitchenSpawnPoint().position;
        transform.parent = kitchenObject.GetKitchenSpawnPoint();
    }


}
