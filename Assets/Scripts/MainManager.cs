using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestscoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private string playerName;
    
    private bool m_GameOver = false;
    private int bestScore = 0;
    private string bestPlayer = "";

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        if (MenuManager.Instance != null)
        {
            playerName = MenuManager.Instance.PlayerName;
            if (MenuManager.Instance.BestScore > 0)
            {
                bestScore = MenuManager.Instance.BestScore;
                bestPlayer = MenuManager.Instance.BestPlayer;
                BestscoreText.text = $"Best Score: {bestScore}  Name: {bestPlayer}";
            }
        }

    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";

        if (m_Points > bestScore)
        {
            BestscoreText.text = $"Best Score: {m_Points}  Name: {playerName}";
            MenuManager.Instance.BestScore = m_Points;
            MenuManager.Instance.BestPlayer = playerName;
            MenuManager.Instance.SaveBestScore();
        }
        else
        {
            BestscoreText.text = $"Best Score: {bestScore}  Name: {bestPlayer}";
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
