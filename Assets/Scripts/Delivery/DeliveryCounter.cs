using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(PlayerInteraction player) {
        if(player.HasKitchenObject()) {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {

                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);

                player.GetKitchenObject().DestroyThis();
            }
        }
    }
}
