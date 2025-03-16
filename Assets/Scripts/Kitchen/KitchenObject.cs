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

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
        if(this is PlateKitchenObject) {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        } else {
            plateKitchenObject = null;
            return false;
        }
    }

    public void DestroyThis() {
        kitchenObject.ClearKitchenObject();
        Destroy(gameObject);
    }
}
