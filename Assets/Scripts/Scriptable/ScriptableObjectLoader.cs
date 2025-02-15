using UnityEngine;

public class ScriptableObjectLoader : MonoBehaviour {
    public static T[] LoadAllScriptableObjects<T>(string path) where T : ScriptableObject {
        T[] allObjects = Resources.LoadAll<T>(path);
        return allObjects;
    }
}
