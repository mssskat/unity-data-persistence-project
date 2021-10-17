using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainUI : MonoBehaviour
{
    [SerializeField] private Text m_UserName;
    [SerializeField] private Text m_BestScore;

    private void Start()
    {
        var userName = GlobalDataSingleton.Instance.recordUserName;
        var bestScore = GlobalDataSingleton.Instance.GetBestScore();
        m_BestScore.text = $"Best Score : {userName} : {bestScore}";
    }

    public void OnUserNameEndEdit()
    {
        GlobalDataSingleton.Instance.userName = m_UserName.text;
    }

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("main");
    }

    public void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
