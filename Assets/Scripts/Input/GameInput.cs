using System;
using TMPro;
using UnityEngine;

public class GameInput : Singleton<GameInput> {
    // Reference to the custom Input System.
    private InputSystem inputSystem;

    // Event triggered when the player performs an interaction.
    public event EventHandler OnPlayerInteract;
    public event EventHandler<bool> OnPlayerCutting;

    // Initializes the input system and sets up the interaction event.
    private void Awake() {
        inputSystem = new InputSystem();
        inputSystem.Player.Enable();

        // Subscribe to the interaction action performed event.
        inputSystem.Player.Interaction.performed += Interactions_performed;
        inputSystem.Player.Cutting.started += Cutting_started;
        inputSystem.Player.Cutting.canceled += Cutting_canceled;
    }

    private void Cutting_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPlayerCutting?.Invoke(this, false);
    }

    private void Cutting_started(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPlayerCutting?.Invoke(this, true);
    }

    // Invokes the OnPlayerInteract event when the interaction input is detected.
    private void Interactions_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPlayerInteract?.Invoke(this, EventArgs.Empty);
    }

    // Returns the player's movement input as a normalized vector.
    public Vector2 GetInputVectorNormalized() {
        Vector2 inputVector = inputSystem.Player.Movement.ReadValue<Vector2>();
        return inputVector.normalized;
    }
}