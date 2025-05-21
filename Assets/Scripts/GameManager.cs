using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject currentLevel;    
    public List<GameObject> levelPrefabs;
    public UnityEvent winEvent;
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
        for (int i = 0; i < DataManager.gameData.playerData.currentLevelIndex; i++)  
        {
            Random.Range(0, 0);
        }
    }

    public void NextLevel()
    {
        bosses.Clear();
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        var temp = new List<GameObject>(levelPrefabs);
        temp.RemoveAt(DataManager.gameData.playerData.currentLevelIndex);
        DataManager.gameData.playerData.currentLevelIndex = Random.Range(0, temp.Count-1);
        GameObject levelPrefab = temp[DataManager.gameData.playerData.currentLevelIndex];
        currentLevel = Instantiate(levelPrefab, Vector3Int.zero, Quaternion.identity);
    }

    public void StartLevel(int index)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        GameObject levelPrefab = levelPrefabs[index];
        currentLevel = Instantiate(levelPrefab, Vector3Int.zero, Quaternion.identity);
    }

    public void OnBossDie(Boss boss)
    {
        if (!bosses.Remove(boss))
        {
            Debug.LogError("Remove boss failed");
        }

        if (bosses.Count == 0)
        {
            Debug.Log("All bosses are dead!");
            Invoke("Win", 1f);
        }
    }

    public void Win()
    {
        Debug.Log("You win!");
        Invoke("delayWin", 1f);
    }

    public void delayWin()
    {
        winEvent?.Invoke();
    }

    void OnApplicationQuit()
    {
        DataManager.Save();
    }
}
