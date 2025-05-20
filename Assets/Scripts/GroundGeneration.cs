using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class GroundGenerator : MonoBehaviour
{
    public List<MeshChance> groundMeshes;
    public Material groundMaterial; 
    public Vector2Int size = new(20, 20);   // Size of the ground in world units
    public float meshSize = 1f;             //mesh size in world units

    [Header("Noise Settings")]
    public Vector2 noiseOffset = Vector2.zero;
    public float noiseScale = 0.1f;
    public float heightMultiplier = 1f;

    void Start()
    {
        GenerateOptimizedMesh();
    }

    [ContextMenu("Generate Mesh")]
    void GenerateOptimizedMesh()
    {
        List<CombineInstance> meshToMerge = new List<CombineInstance>();

        for (int x = 0; x < size.x; x++)
        {
            for (int z = 0; z < size.y; z++)
            {
                float noise = Mathf.PerlinNoise((x + noiseOffset.x) * noiseScale, (z + noiseOffset.y) * noiseScale);
                int height = Mathf.RoundToInt(noise * heightMultiplier);
                
                MeshChance meshChance = MeshUtility.GetRandomMesh(groundMeshes);
                Vector3 pos = new Vector3(x * meshSize, height * meshSize, z * meshSize);
                int randomAngle = meshChance.randomRotation ? 90*Random.Range(0, 4) : 0;
                Quaternion rotation = Quaternion.Euler(0, randomAngle, 0);

                CombineInstance ci = new CombineInstance();
                ci.mesh = meshChance.mesh;

                ci.transform = Matrix4x4.TRS(pos, rotation, Vector3.one);

                meshToMerge.Add(ci);
            }
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32; // <-- Add this line
        combinedMesh.CombineMeshes(meshToMerge.ToArray());

        GetComponent<MeshFilter>().mesh = combinedMesh;
        GetComponent<MeshRenderer>().material = groundMaterial;
        GetComponent<MeshCollider>().sharedMesh = combinedMesh;
    }
}

[System.Serializable]
public class MeshChance
{
    public Mesh mesh;
    [Range(0, 1)] public float chance = 0.5f;
    public bool randomRotation = true;

    public MeshChance(Mesh mesh, float chance, bool randomRotation = true)
    {
        this.mesh = mesh;
        this.chance = chance;
        this.randomRotation = randomRotation;
    }
}
