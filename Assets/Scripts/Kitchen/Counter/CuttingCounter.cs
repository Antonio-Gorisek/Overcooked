using System;
using System.Linq;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    private bool isPlayerCutting;
    private float cuttingTime;
    private CuttingRecipeSO[] cuttingRecipes;
    private CuttingRecipeSO currentRecipe;
    public event EventHandler<bool> OnPlayerCutItem;

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs {
        public float progress;
    }

    private void Start() {
        cuttingRecipes = ScriptableObjectLoader.LoadAllScriptableObjects<CuttingRecipeSO>("ScriptableObjects/CuttingRecipe");
    }

    public override void Interact(PlayerInteraction player) {
        if (player.HasKitchenObject() == true && HasKitchenObject() == false) {
            player.GetKitchenObject().SetKitchenObject(this);
        } else if (player.HasKitchenObject() == false && HasKitchenObject() == true) {
            GetKitchenObject().SetKitchenObject(player);
        }
    }

    public override void CuttingInteract(PlayerInteraction player, bool isCutting) {
        if(HasKitchenObject()) {
            isPlayerCutting = isCutting;

            if(isCutting) {
                currentRecipe = GetCuttedItem(GetKitchenObject().GetKitchenItem());
            }
            else {
                currentRecipe = null;
            }
        }
    }

    private CuttingRecipeSO GetCuttedItem(KitchenItemSO kitchenObjectSO) {
        return cuttingRecipes.FirstOrDefault(recipe => recipe.uncutItem == kitchenObjectSO);
    }


    private void Update() {
        if (isPlayerCutting == false || currentRecipe == null || HasKitchenObject() == false) {
            ResetCutting();
            return;
        }

        cuttingTime += Time.deltaTime;
        OnPlayerCutItem?.Invoke(this, true);

        float progress = Mathf.Clamp01(cuttingTime / currentRecipe.cuttingTime);
        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { progress = progress });

        if (cuttingTime >= currentRecipe.cuttingTime) {
            ResetCutting();
            CutItem(currentRecipe);
        }
    }

    void ResetCutting() {
        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { progress = 0 });

        OnPlayerCutItem?.Invoke(this, false);
        cuttingTime = 0;
        isPlayerCutting = false;
    }


    public void CutItem(CuttingRecipeSO recipeSO) {
        KitchenItemSO cuttedItem = recipeSO.cutItem;
        GetKitchenObject().DestroyThis();

        Transform kitchenObject = Instantiate(cuttedItem.itemPrefb, GetKitchenSpawnPoint());
        kitchenObject.GetComponent<KitchenObject>().SetKitchenObject(this);
    }

}
