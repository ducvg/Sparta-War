using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int currentLevelIndex = 0;
    public List<GameObject> levelPrefabs;
    public GameObject WinPanel;
    public List<Boss> bosses = new List<Boss>();

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Time.timeScale = 1;
        Random.InitState("SkibidiRizz".GetHashCode());
    }

    public void NextLevel()
    {
        var temp = new List<GameObject>(levelPrefabs);
        temp.RemoveAt(currentLevelIndex);
        int levelIndex = Random.Range(0, levelPrefabs.Count);
        GameObject levelPrefab = temp[levelIndex];
        Instantiate(levelPrefab);
    }

    public void onBossDie(Boss boss)
    {
        if (!bosses.Remove(boss))
        {
            Debug.LogError("Remove boss failed");
        }

        if (bosses.Count == 0)
            {
                Debug.Log("All bosses are dead!");

            }
    }

    public void Win()
    {
        Debug.Log("You win!");
        WinPanel.SetActive(true);
        Time.timeScale = 0;
    }


}
