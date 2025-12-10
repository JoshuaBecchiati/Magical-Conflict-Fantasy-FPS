using UnityEngine;
using UnityEngine.AI;

public class TerrainTreesToObjects : MonoBehaviour
{
    public Terrain terrain;

    void Start()
    {
        if (terrain == null) terrain = GetComponent<Terrain>();

        var data = terrain.terrainData;
        var trees = data.treeInstances;

        for (int i = 0; i < trees.Length; i++)
        {
            TreeInstance tree = trees[i];
            GameObject prefab = data.treePrototypes[tree.prototypeIndex].prefab;

            Vector3 worldPos = Vector3.Scale(tree.position, data.size) + terrain.transform.position;

            GameObject obj = Instantiate(prefab, worldPos, Quaternion.identity, transform);

            // aggiungo NavMeshObstacle
            var col = obj.GetComponent<Collider>();
            if (col == null) obj.AddComponent<CapsuleCollider>();

            var obstacle = obj.AddComponent<NavMeshObstacle>();
            obstacle.carving = true;
        }
    }
}

