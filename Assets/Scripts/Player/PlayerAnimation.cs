using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private PlayerMovement _playerMovement;
    private Animator _playerAnimator;

    private void Awake() {
        _playerMovement = transform.parent.GetComponent<PlayerMovement>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update() => _playerAnimator.SetBool(IS_WALKING, _playerMovement.IsWalking());


}
