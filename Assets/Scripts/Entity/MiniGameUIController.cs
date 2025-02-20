using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameUIController : MonoBehaviour
{
    [SerializeField] private GameObject miniGamePanel;

    [SerializeField] private int bestScore;

    void Update()
    {
        if (miniGamePanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                PlayerPrefs.SetInt("BestScore", bestScore);
                PlayerPrefs.Save();
                SceneManager.LoadScene("FlappyScene");
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                miniGamePanel.SetActive(false);
            }
        }
    }
}
