using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GlobalDataSingleton : MonoBehaviour
{
    public static GlobalDataSingleton Instance;

    public string userName;
    public string recordUserName;

    public int GetBestScore()
    {
        return m_BestScore;
    }

    public void SetBestScore(int value)
    {
        if(value > m_BestScore)
        {
            recordUserName = userName;
            m_BestScore = value;
            SaveBestScore();
        }
    }

    private int m_BestScore;

    [System.Serializable]
    private class BestScoreData
    {
        public string userName;
        public int bestScore;

        public BestScoreData(string _user, int _score)
        {
            userName = _user;
            bestScore = _score;
        }
    }

    private void SaveBestScore()
    {
        BestScoreData data = new BestScoreData(recordUserName, m_BestScore);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    private void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            BestScoreData data = JsonUtility.FromJson<BestScoreData>(json);
            recordUserName = data.userName;
            m_BestScore = data.bestScore;
        }
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadBestScore();
    }
}
