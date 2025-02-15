using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(PlayerInteraction player) {
        if(player.HasKitchenObject()) {
            player.GetKitchenObject().DestroyThis();
        }
    }
}
