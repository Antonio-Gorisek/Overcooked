using UnityEngine;
using UnityEngine.UI;

public class DeliveryRecipeUI : MonoBehaviour
{

    [SerializeField] private Transform icon;

    private void Awake() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }

    public void SetRecipeSO(RecipeSO recipeSO) {

        foreach(KitchenItemSO kitchenObjectSO in recipeSO.kitchenItemSoList) {
            Transform iconTransfrom = Instantiate(icon, transform);
            iconTransfrom.GetComponent<Image>().sprite = kitchenObjectSO.icon;
        }
    }
}
