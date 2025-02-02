using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    private Animator playerAnimator;

    void Awake() => playerAnimator = GetComponent<Animator>();

    void Update() => playerAnimator.SetBool("Walking", playerMovement.isWalking());
}
