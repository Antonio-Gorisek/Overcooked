using UnityEngine;

public class GameInput : Singleton<GameInput>
{
    private InputSystem inputSystem;

    private void Awake() {
        inputSystem = new InputSystem();
        inputSystem.Player.Enable();
    }

    public Vector2 GetInputVectorNormalized() {
        Vector2 inputVector = inputSystem.Player.Movement.ReadValue<Vector2>();
        return inputVector.normalized;
    }
}
