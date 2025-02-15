using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private CuttingCounter cuttingCounter;

    private void Start() {
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e) {
        slider.value = e.progress;

        slider.gameObject.SetActive(e.progress > 0);
    }
}
