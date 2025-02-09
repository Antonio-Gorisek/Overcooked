using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenItemSO kitchenItemSO;

    public override void Interact(PlayerInteraction player) {
       
        if(player.HasKitchenObject() == true && HasKitchenObject() == false) 
        {
            player.GetKitchenObject().SetKitchenObject(this);
        } 
        else if (player.HasKitchenObject() == false && HasKitchenObject() == true) 
        {
            GetKitchenObject().SetKitchenObject(player);
        }
    }

}
