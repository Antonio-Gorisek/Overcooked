using UnityEngine;

public class ContainerAnimation : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    private Animator animator;
    private ContainerCounter containerCounter;

    private void Awake() {
        animator = GetComponent<Animator>();
        containerCounter = GetComponent<ContainerCounter>();
    }

    private void Start() {
        containerCounter.OnPlayerGetObject += ContainerCounter_OnPlayerGetObject;
    }

    private void ContainerCounter_OnPlayerGetObject(object sender, System.EventArgs e) {
        animator.SetTrigger("OpenClose");
    }
}
