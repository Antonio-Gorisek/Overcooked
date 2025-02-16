using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    private StoveCounter stoveCounter;
    [SerializeField] private GameObject prticleGameObject;
    [SerializeField] private GameObject stoveGameObject;

    private void Awake() {
        stoveCounter = GetComponent<StoveCounter>();
    }

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.CookingState state) {

        bool startEffect = state == StoveCounter.CookingState.Cooking || state == StoveCounter.CookingState.Cooked;
        prticleGameObject.SetActive(startEffect);
        stoveGameObject.SetActive(startEffect);
    }
}
