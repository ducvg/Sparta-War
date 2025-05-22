using System;
using System.Collections;
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

    [SerializeField] private List<GameObject> halfLevels1, halfLevels2 = new List<GameObject>();

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
        // for (int i = 0; i < DataManager.gameData.playerData.currentLevelIndex; i++)
        // {
        //     Random.Range(0, 0);
        // }
        levelPrefabs.Shuffle();

        SplitLevels(levelPrefabs);
    }
    
    private void SplitLevels(List<GameObject> bigLevels)
    {
        int half = bigLevels.Count / 2;

        halfLevels1 = bigLevels.GetRange(0, half);
        halfLevels2 = bigLevels.GetRange(half, bigLevels.Count - half);
    }


    public void NextLevel()
    {
        bosses.Clear();
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        var levelIndex = DataManager.gameData.playerData.currentLevelIndex;
        levelIndex++;
        if(levelIndex >= levelPrefabs.Count)
        {
            levelIndex = 0;
        }

        GameObject levelPrefab = levelPrefabs[levelIndex];
        currentLevel = Instantiate(levelPrefab, Vector3Int.zero, Quaternion.identity);
        DataManager.gameData.playerData.currentLevelIndex = levelIndex;

        // var temp = new List<GameObject>(levelPrefabs);
        // temp.RemoveAt(DataManager.gameData.playerData.currentLevelIndex);
        // DataManager.gameData.playerData.currentLevelIndex = Random.Range(0, temp.Count - 1);
        // GameObject levelPrefab = temp[DataManager.gameData.playerData.currentLevelIndex];
        // currentLevel = Instantiate(levelPrefab, Vector3Int.zero, Quaternion.identity);
    }

    public void StartLevel(int index)
    {
        bosses.Clear();
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        if (index < 0 || index >= levelPrefabs.Count)
        {
            Debug.LogError("Invalid level index");
            index = 0;
        }

        GameObject levelPrefab = levelPrefabs[index];
        currentLevel = Instantiate(levelPrefab, Vector3Int.zero, Quaternion.identity);
        DataManager.gameData.playerData.currentLevelIndex = index;
    }

    public void OnBossDie(Boss boss)
    {
        if (!bosses.Remove(boss))
        {
            Debug.LogError("Remove boss failed");
        }

        if (bosses.Count == 0)
        {
            GameState.IsGameWon = true;
            Debug.Log("All bosses are dead!");
            StartCoroutine(DelayWin(1.5f));
        }
    }

    private IEnumerator DelayWin(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Win");
        winEvent?.Invoke();
    }



    void OnApplicationQuit()
    {
        DataManager.Save();
    }
}
