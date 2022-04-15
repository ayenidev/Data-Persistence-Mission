using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string BestPlayer="";
    public int BestScore= 0;
    public string PlayerName= "";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadBestScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string BestPlayer;
        public int BestScore;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.BestPlayer = BestPlayer;
        data.BestScore = BestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/dataPeristencefile.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/dataPeristencefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestPlayer = data.BestPlayer;
            BestScore = data.BestScore;
        }
    }
}
