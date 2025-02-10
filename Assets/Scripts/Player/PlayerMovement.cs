using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float _movementSpeed = 15;
    [SerializeField] private float _rotationSpeed = 10;
    private bool isWalking = false;

    void Update() => Movement();

    private void Movement() {
        Vector2 inputVector = GameInput.Instance.GetInputVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        // Check if the player can move in the desired direction
        bool canMove = CanMoveInDirection(ref moveDir);

        // Move the player if possible
        if (canMove) {
            transform.position += moveDir * _movementSpeed * Time.deltaTime;
        }

        // Smoothly rotate the player towards the input direction, even if the player can't move
        if (inputVector != Vector2.zero) {
            Vector3 lookDir = new Vector3(inputVector.x, 0, inputVector.y);
            transform.forward = Vector3.Slerp(transform.forward, lookDir, _rotationSpeed * Time.deltaTime);
        }

        // Update walking status
        isWalking = moveDir != Vector3.zero;
    }

    // Checks if the player can move in the given direction or adjusts the direction
    private bool CanMoveInDirection(ref Vector3 moveDir) {
        // Check movement in the given direction if there is no obstacle
        if (!GetCapsuleCast(moveDir)) {
            return true;
        }

        // Check movement along the X-axis only
        Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
        if (!GetCapsuleCast(moveDirX)) {
            moveDir = moveDirX;
            return true;
        }

        // Check movement along the Z-axis only
        Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
        if (!GetCapsuleCast(moveDirZ)) {
            moveDir = moveDirZ;
            return true;
        }

        return false;  // No valid direction for movement
    }

    // Performs a capsule cast to detect obstacles in the given direction.
    // Returns true if an obstacle is hit, false if the path is clear.
    private bool GetCapsuleCast(Vector3 moveDir) {
        float moveDistance = _movementSpeed * Time.deltaTime;
        float playerRadius = 0.5f;
        float playerHeight = 2;

        // Check for obstacles in the direction of movement
        bool isObstacle = Physics.CapsuleCast(
            transform.position,
            transform.position + Vector3.up * playerHeight,
            playerRadius,
            moveDir,
            moveDistance
        );

        return isObstacle; // Returns true if an obstacle is detected
    }

    public bool IsWalking() => isWalking;
}
