using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{

    public TextMeshProUGUI bestScoreText;
    public InputField playerNameInputField;

    void Start()
    {
        if (MenuManager.Instance != null)
        {
            if (MenuManager.Instance.BestScore > 0)
            {
                var bestScore = MenuManager.Instance.BestScore;
                var bestPlayer = MenuManager.Instance.BestPlayer;
                bestScoreText.text = $"Best Score: {bestScore}  Name: {bestPlayer}";
            }
            else
            {
                bestScoreText.text = "Best Score: 0";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNew()
    {
        MenuManager.Instance.PlayerName = playerNameInputField.text;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
