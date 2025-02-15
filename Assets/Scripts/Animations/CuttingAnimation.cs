using UnityEngine;

public class CuttingAnimation : MonoBehaviour
{
    private const string CUTTING = "Cut";
    private Animator animator;
    private CuttingCounter cuttingCounter;

    private void Awake() {
        animator = GetComponent<Animator>();
        cuttingCounter = GetComponent<CuttingCounter>();
    }

    private void Start() {
        cuttingCounter.OnPlayerCutItem += CuttingCounter_OnPlayerCutItem; ;
    }

    private void CuttingCounter_OnPlayerCutItem(object sender, bool value) {
        animator.SetBool(CUTTING, value);
    }
}
