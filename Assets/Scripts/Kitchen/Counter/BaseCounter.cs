using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObject
{
    [SerializeField] private Transform counerSpawnPoint;
    [NonSerialized] private KitchenObject kitchenObject;

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    } 

    public Transform GetKitchenSpawnPoint() {
        return counerSpawnPoint;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }


    public virtual void Interact(PlayerInteraction player) {

    }
    
    public virtual void CuttingInteract(PlayerInteraction player, bool isCutting) {

    }

}
