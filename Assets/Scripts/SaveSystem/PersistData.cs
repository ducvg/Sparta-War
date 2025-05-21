using UnityEngine;

public class PersistData : MonoBehaviour
{
    public bool allowLoad = false;
    public bool allowSave = true;
    public static bool isLoaded = false;

    public static PersistData instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        if (allowLoad && !isLoaded)
        {
            isLoaded = true;
            if(DataManager.Load())
            {
                Debug.Log("Game Loaded");
            }
            else
            {
                Debug.LogError("Failed to load game data. \nStart new game");
            }
        }
        else
        {
            Debug.Log("New game ");
        }
    }
}
