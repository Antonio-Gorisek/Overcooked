using System;
using System.Diagnostics;
using System.Linq;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class StoveCounter : BaseCounter, IProgressBar
{
    public event EventHandler<CookingState> OnStateChanged;
    public event EventHandler<IProgressBar.OnProgressChangedEventArgs> OnProgressChanged;

    private CookingRecipeSO[] cookingRecipeSO;
    private CookingRecipeSO currentRecipe;
    private float cookingTime;
    
    private BurningRecipeSO[] burnedRecepieSO;
    private BurningRecipeSO currentBurnedRecipe;
    private float burningTime;

    public enum CookingState { Idle, Cooking, Cooked, Burned }

    public CookingState state;
    


    private void Start() {

        cookingRecipeSO = ScriptableObjectLoader.LoadAllScriptableObjects<CookingRecipeSO>("ScriptableObjects/CookingRecipe");
        burnedRecepieSO = ScriptableObjectLoader.LoadAllScriptableObjects<BurningRecipeSO>("ScriptableObjects/BurnedRecipe");
        state = CookingState.Idle;
    }

    private void Update() {
        switch(state) {
            case CookingState.Idle:
                ProgressChange(0);
                break;
            case CookingState.Cooking:
                Cooking();
                break;
            case CookingState.Cooked:
                Burning();
                break;
            case CookingState.Burned:
                ProgressChange(0);
                break;
        }
    }


    private void Cooking() {
        if (HasKitchenObject()) {
            cookingTime += Time.deltaTime;

            float progress = Mathf.Clamp01(cookingTime / currentRecipe.cookingTime);
            ProgressChange(progress);
            if (cookingTime >= currentRecipe.cookingTime) {
                GetKitchenObject().DestroyThis();

                state = CookingState.Cooked;
                OnStateChanged?.Invoke(this, state);

                Transform kitchenObject = Instantiate(currentRecipe.cookedItem.itemPrefb, GetKitchenSpawnPoint());
                kitchenObject.GetComponent<KitchenObject>().SetKitchenObject(this);
                currentBurnedRecipe = GetBurnedRecipeSO(GetKitchenObject().GetKitchenItem());
            }
        }
    }

    private void Burning() {
        if(HasKitchenObject()) {
            burningTime += Time.deltaTime;

            float progress = Mathf.Clamp01(burningTime / currentBurnedRecipe.burningTime);
            ProgressChange(progress);
            if (burningTime >= currentBurnedRecipe.burningTime) {
                GetKitchenObject().DestroyThis();

                state = CookingState.Burned;
                OnStateChanged?.Invoke(this, state);
                
                Transform kitchenObject = Instantiate(currentBurnedRecipe.burnedItem.itemPrefb, GetKitchenSpawnPoint());
                kitchenObject.GetComponent<KitchenObject>().SetKitchenObject(this);
            }
        }
    }

    public override void Interact(PlayerInteraction player) {
       if(HasKitchenObject() == false) {
            if(player.HasKitchenObject()) {
                if (GetCookingRecipe(player.GetKitchenObject().GetKitchenItem())) {
                    player.GetKitchenObject().SetKitchenObject(this);

                    state = CookingState.Cooking;
                    OnStateChanged?.Invoke(this, state);
                    currentRecipe = GetCookingRecipe(GetKitchenObject().GetKitchenItem());
                    OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs { progress = 0 });

                    cookingTime = 0;
                    burningTime = 0;
                }  
            }
        } else {
            if(player.HasKitchenObject() == false) {
                GetKitchenObject().SetKitchenObject(player);

                state = CookingState.Idle;
                OnStateChanged?.Invoke(this, state);
            }
            else {
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenItem())) {
                        GetKitchenObject().DestroyThis();

                        cookingTime = 0;
                        burningTime = 0;
                        state = CookingState.Idle;
                        OnStateChanged?.Invoke(this, state);
                    };
                }
            }
        }
    }

    private void ProgressChange(float time) {
        OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs { progress = time });
    }

    private CookingRecipeSO GetCookingRecipe(KitchenItemSO kitchenObjectSO) {
        return cookingRecipeSO.FirstOrDefault(recipe => recipe.uncookedItem == kitchenObjectSO);
    }

    private BurningRecipeSO GetBurnedRecipeSO(KitchenItemSO kitchenObjectSO) {
        return burnedRecepieSO.FirstOrDefault(recipe => recipe.cookedItem == kitchenObjectSO);
    }

}
