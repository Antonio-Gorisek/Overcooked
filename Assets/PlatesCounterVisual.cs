using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private Transform spawnPoint;

    private List<GameObject> platesVisualList = new List<GameObject>();

    private void Start() {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e) {
        GameObject plateObject = platesVisualList[platesVisualList.Count -1];
        platesVisualList.Remove(plateObject);
        Destroy(plateObject);

    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e) {

        float plateOffSetY = 0.1f;
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, spawnPoint);

        plateVisualTransform.localPosition = new Vector3(0, plateOffSetY * platesVisualList.Count, 0);
        platesVisualList.Add(plateVisualTransform.gameObject);
    }
}
