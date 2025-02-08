using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component {
    // Static instance of the singleton.
    private static T instance;

    // Public property to access the singleton instance.
    public static T Instance {
        get {
            // If the instance is not set, find it in the scene.
            if (instance == null) {
                instance = FindFirstObjectByType<T>();

                // If the instance is still not found, create a new GameObject and attach the component.
                if (instance == null) {
                    GameObject gameObject = new GameObject("SingletonController");
                    instance = gameObject.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    private void Awake() {
        // If no instance exists, assign this object as the instance.
        if (instance == null) {
            instance = this as T;
        } else {
            // If an instance already exists, destroy the duplicate.
            Destroy(gameObject);
        }
    }
}
