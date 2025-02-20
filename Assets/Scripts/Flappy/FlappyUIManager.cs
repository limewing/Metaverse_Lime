using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlappyUIManager : MonoBehaviour
{
    
    public TextMeshProUGUI restartText;

    [SerializeField] private TMP_Text scoreText; 
    [SerializeField] private TMP_Text bestScoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (restartText == null)
            Debug.LogError("restart text is null");

        if (scoreText == null)
            Debug.LogError("score text is null");

        restartText.gameObject.SetActive(false);
    }

    public void SetRestart()
    {
        restartText.gameObject.SetActive(true);
    }

    public void UpdateScore(int currentScore)
    {
        scoreText.text = "Score: " + currentScore.ToString();

        // PlayerPrefs에 저장된 BestScore 값을 읽어와 업데이트
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "Best: " + bestScore.ToString();
    }
}
