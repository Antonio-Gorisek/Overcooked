using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem inputSystem;
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float rotationSpeed = 5;
    private bool isPlayerWalking = false;

    void Start() {
        inputSystem = new InputSystem();
        inputSystem.Player.Enable();
    }



    void Update() {
        Vector2 inputVector = GetInputVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        if(CanMoveInDirection(ref moveDir)) {
            transform.position += moveDir * movementSpeed * Time.deltaTime;
        }

        if(moveDir != Vector3.zero) {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);
        }

        isPlayerWalking = moveDir != Vector3.zero;
    }


    private bool CanMoveInDirection(ref Vector3 moveDir) {
        if (GetCapsuleCast(moveDir)) {
            return true;
        }

        Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
        if(GetCapsuleCast(moveDirX)) {
            moveDir = moveDirX;
            return true;
        }

        Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
        if (GetCapsuleCast(moveDirZ)) {
            moveDir = moveDirZ;
            return true;
        }


        return false;
    }


    private bool GetCapsuleCast(Vector3 moveDir) {

        float moveDistance = movementSpeed * Time.deltaTime;
        float playerHeight = 2;
        float playerRadius = 0.3f;
        return !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, moveDir, moveDistance);
    }


    public Vector2 GetInputVector() {
        return inputSystem.Player.Movement.ReadValue<Vector2>().normalized;
    }

    public bool isWalking() {
        return isPlayerWalking;
    }
    
}
