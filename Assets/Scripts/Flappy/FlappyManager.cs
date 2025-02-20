using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappyManager : MonoBehaviour
{
    static FlappyManager flappyManager;
    public static FlappyManager Instance { get { return flappyManager; } }

    private int currentScore = 0;
    private bool isGameOver = false;
    private int currentBest = 0;

    FlappyUIManager uiManager;
    public FlappyUIManager UIManager { get { return uiManager; } }

    private void Awake()
    {
        flappyManager = this;
        uiManager = FindObjectOfType<FlappyUIManager>();
        currentBest = PlayerPrefs.GetInt("BestScore", 0);
    }

    private void Start()
    {
        uiManager.UpdateScore(0);
    }

    void Update()
    {
        if (isGameOver)
        {
            uiManager.SetRestart();
            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene("SampleScene");
            }
            else if (Input.GetMouseButtonDown(0))
            {
                RestartGame();
            }
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        isGameOver = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log("Score: " + currentScore);
        uiManager.UpdateScore(currentScore);

        if (currentScore > currentBest)
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            PlayerPrefs.Save(); // 즉시 저장하여 앱 종료 후에도 유지되게 함
        }
    }

}
