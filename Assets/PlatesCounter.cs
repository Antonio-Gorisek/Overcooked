using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenItemSO kitchenObjectSO;
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    private float spawnPlateTimer;
    private float maxSpawnPlateTimer = 5;

    private int platesSpawnedAmount;
    private int platesSpawnedMaxed = 4;

    private void Update() {
        spawnPlateTimer += Time.deltaTime;

        if(spawnPlateTimer > maxSpawnPlateTimer) {
            spawnPlateTimer = 0;

            if(platesSpawnedAmount < platesSpawnedMaxed) {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(PlayerInteraction player) {
        if (!player.HasKitchenObject() && platesSpawnedAmount > 0) {

            platesSpawnedAmount--;
            Transform kitchenObjectTransfrom = Instantiate(kitchenObjectSO.itemPrefb, GetKitchenSpawnPoint());
            kitchenObjectTransfrom.GetComponent<KitchenObject>().SetKitchenObject(player);

            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
        }
    }
}
