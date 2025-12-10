using UnityEngine;
using UnityEditor;

public class ReplaceWithPrefab : MonoBehaviour
{
    [MenuItem("Tools/Replace Selected With Prefab")]
    static void Replace()
    {
        GameObject prefab = Selection.activeObject as GameObject; // primo prefab selezionato
        if (prefab == null || PrefabUtility.GetPrefabAssetType(prefab) == PrefabAssetType.NotAPrefab)
        {
            Debug.LogWarning("Seleziona un prefab valido prima!");
            return;
        }

        foreach (GameObject go in Selection.gameObjects)
        {
            // Salva trasformazioni
            Vector3 pos = go.transform.position;
            Quaternion rot = go.transform.rotation;
            Vector3 scale = go.transform.localScale;

            // Cancella vecchio
            GameObject.DestroyImmediate(go);

            // Istanzia prefab
            GameObject newObj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            newObj.transform.position = pos;
            newObj.transform.rotation = rot;
            newObj.transform.localScale = scale;
        }
    }
}

