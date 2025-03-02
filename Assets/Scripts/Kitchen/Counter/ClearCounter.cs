using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenItemSO kitchenItemSO;

    public override void Interact(PlayerInteraction player) {
       
        if(!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetKitchenObject(this);
            }
        } else {
            if (player.HasKitchenObject()) {
                if(player.GetKitchenObject() is PlateKitchenObject) {

                    PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenItem())) {
                        GetKitchenObject().DestroyThis();
                    };
                }
            }
            else {
                GetKitchenObject().SetKitchenObject(player);
            }
        }
    }

}
