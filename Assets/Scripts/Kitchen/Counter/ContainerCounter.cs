using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {

    public event EventHandler OnPlayerGetObject;
    [SerializeField] private KitchenItemSO kitchenItemSO;

    public override void Interact(PlayerInteraction player) {
        if(player.HasKitchenObject() == false) {
            Transform kitchenObjectTransfrom = Instantiate(kitchenItemSO.itemPrefb, GetKitchenSpawnPoint());
            kitchenObjectTransfrom.GetComponent<KitchenObject>().SetKitchenObject(player);
            OnPlayerGetObject?.Invoke(this, EventArgs.Empty);
        }
    }

}
