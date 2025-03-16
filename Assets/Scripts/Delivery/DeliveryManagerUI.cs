using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Start() {
        DeliveryManager.Instance.OnRecipeCompleted += Instance_OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeSpawned += Instance_OnRecipeSpawned;
        UpdateVisual();
    }

    private void Instance_OnRecipeSpawned(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void Instance_OnRecipeCompleted(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in container) {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }


        foreach(RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSoList()) {
            Transform recipeTransfrom = Instantiate(recipeTemplate, container);

            recipeTransfrom.GetComponent<DeliveryRecipeUI>().SetRecipeSO(recipeSO);
        }
    }
}
