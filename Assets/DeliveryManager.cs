using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : Singleton<DeliveryManager>
{

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSoList = new List<RecipeSO>();



    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4;
    private float waitingRecipesMax = 4;


    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;

        if(spawnRecipeTimer <= 0) {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingRecipeSoList.Count < waitingRecipesMax) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSoList.Add(waitingRecipeSO);

                Debug.Log(waitingRecipeSO.recipeName);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public List<RecipeSO> GetWaitingRecipeSoList() {
        return waitingRecipeSoList;
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        bool ingredinetFound = false;
        for (int i = 0; i < waitingRecipeSoList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSoList[i];

            if(waitingRecipeSO.kitchenItemSoList.Count == plateKitchenObject.GetKitchenItemSOList().Count) {
                foreach (KitchenItemSO recipeKithcenObjectSO in waitingRecipeSO.kitchenItemSoList) {
                    foreach(KitchenItemSO plateKitchenObjectSO in plateKitchenObject.GetKitchenItemSOList()) {

                        if(plateKitchenObjectSO == recipeKithcenObjectSO) {
                            ingredinetFound = true;
                            break;
                        }
                    }
                }
            }

            if (ingredinetFound) {
                Debug.Log("Player delivered the correct recipe!");
                waitingRecipeSoList.RemoveAt(i);

                OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                return;
            }
        }

        if(ingredinetFound == false) {
            Debug.Log("Wrong recipe");
        }
    }
}
