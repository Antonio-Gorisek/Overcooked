using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject progressBarGO;

    private IProgressBar progressBar;


    private void Awake() {
        progressBar = progressBarGO.GetComponent<IProgressBar>();
    }

    private void Start() {
        progressBar.OnProgressChanged += ProgressBar_OnProgressChanged;
    }

    private void ProgressBar_OnProgressChanged(object sender, IProgressBar.OnProgressChangedEventArgs e) {
        slider.value = e.progress;

        slider.gameObject.SetActive(e.progress > 0);
    }
}
