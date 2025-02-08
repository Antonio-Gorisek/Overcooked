using UnityEngine;

public class VisualCounter : MonoBehaviour {
    // Reference to the GameObject that will be highlighted when selected.
    [SerializeField] private GameObject selectedGameObject;

    // Reference to the specific ClearCounter this script will monitor.
    [SerializeField] private ClearCounter clearCounter;

    // Subscribes to the OnSelectedCounter event when the script starts.
    private void Start() {
        PlayerInteraction.Instance.OnSelectedCounter += Instance_OnSelectedCounter;
    }

    // Event handler that activates or deactivates the selectedGameObject
    // based on whether the selected counter matches the clearCounter.
    private void Instance_OnSelectedCounter(object sender, PlayerInteraction.OnSelectedCounterEventArgs e) {
        selectedGameObject.SetActive(e.selectedCounter == clearCounter);
    }
}