using System.Collections.Generic;
using UnityEngine;

public static class MeshUtility
{
    public static MeshChance GetRandomMesh(List<MeshChance> meshChances)
    {
        float total = 0f;
        foreach (var chance in meshChances)
        {
            total += chance.chance; //incase total chance isnt 1, scale with it
        }

        float rand = Random.Range(0f, total);
        float cumulative = 0f;

        
        
        foreach (var mesh in meshChances)
        {
            cumulative += mesh.chance;
            if (rand <= cumulative) return mesh;
        }

        // Fallback with first mesh 
        return meshChances[0];
    }
}