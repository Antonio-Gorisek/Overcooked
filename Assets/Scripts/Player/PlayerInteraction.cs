using System;
using UnityEngine;

public class PlayerInteraction : Singleton<PlayerInteraction>, IKitchenObject {

    [SerializeField] private Transform holdPoint;
    private KitchenObject kitchenObject;

    // LayerMask to define which layers the player can interact with.
    [SerializeField] private LayerMask layerMask;

    // Currently selected counter that the player is interacting with.
    private BaseCounter baseCounter;

    // Last direction in which the player interacted.
    private Vector3 lastInteraction;

    // Event triggered when a new counter is selected.
    public event EventHandler<OnSelectedCounterEventArgs> OnSelectedCounter;
    public class OnSelectedCounterEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    // Subscribes to the player interaction event at the start.
    private void Start() => GameInput.Instance.OnPlayerInteract += Instance_OnPlayerInteract;


    // Trigger interaction logic when the player presses the "E" key.
    private void Instance_OnPlayerInteract(object sender, System.EventArgs e) {
        baseCounter?.Interact(this);
    }

    // Continuously checks for player interaction each frame.
    void Update() => Interaction();


    // Handles the logic for detecting interactable objects.
    private void Interaction() {

        // Get normalized input vector from player input.
        Vector2 inputVector = GameInput.Instance.GetInputVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        // Update last interaction direction if there is movement.
        if (moveDir != Vector3.zero) {
            lastInteraction = moveDir;
        }

        int interactionDistance = 1;
        // Cast a ray to detect objects within interaction distance.
        if (Physics.Raycast(transform.position, lastInteraction, out RaycastHit raycastHit, interactionDistance, layerMask)) {
            // Check if the hit object has a ClearCounter component.
            if (raycastHit.transform.TryGetComponent(out BaseCounter clearCounter)) {

                // Select the counter if it's different from the currently selected one.
                if (baseCounter != clearCounter) {
                    OnSelectCounter(clearCounter);
                }
            } else {
                // Deselect if the object doesn't have a ClearCounter.
                OnSelectCounter(null);
            }
        } else {
            // Deselect if nothing is hit.
            OnSelectCounter(null);
        }
    }

    // Handles selecting and deselecting counters.
    private void OnSelectCounter(BaseCounter clearCounter) {
        baseCounter = clearCounter;
        OnSelectedCounter?.Invoke(this, new OnSelectedCounterEventArgs {
            selectedCounter = clearCounter
        });
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public Transform GetKitchenSpawnPoint() {
        return holdPoint;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

}