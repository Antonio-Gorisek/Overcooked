using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    // Parameter name for walking animation.
    private const string IS_WALKING = "IsWalking";

    // Reference to the player movement script.
    private PlayerMovement _playerMovement;

    // Reference to the Animator component.
    private Animator _playerAnimator;

    // Initialization of references.
    private void Awake() {
        _playerMovement = transform.parent.GetComponent<PlayerMovement>();
        _playerAnimator = GetComponent<Animator>();
    }

    // Updates animation based on movement.
    private void Update() {
        _playerAnimator.SetBool(IS_WALKING, _playerMovement.IsWalking());
    }
}
