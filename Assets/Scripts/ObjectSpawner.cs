using UnityEngine;
using System;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] Objects;

    private void Awake()
    {
        System.Random random = new System.Random();
        int randomIndex = random.Next(0, Objects.Length);
        var obj = Instantiate(Objects[randomIndex], transform.position, Quaternion.identity, transform);

        Quaternion rotation = Quaternion.Euler(0, (float)random.NextDouble() * 360, 0);
        obj.transform.rotation = rotation;

    }
}
