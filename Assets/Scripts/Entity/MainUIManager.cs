using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager Instance { get; private set; }

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject nameBox;
    [SerializeField] private Image nameImage;
    [SerializeField] private TMP_Text npcNameText;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Image dialogueImage;
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private GameObject miniGamePanel;
    [SerializeField] private Text miniGameNameText;
    [SerializeField] private Image miniGameImage;
    [SerializeField] private Text bestScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
            

        if (dialoguePanel == null)
        {
            Debug.LogError("MainUIManager에 DialoguePanel이 설정되지 않았습니다");
        }

    }

    public void ShowDialogue(string npcName, string dialogue)
    {
        Debug.Log($"ShowDialogue() 실행됨 - NPC: {npcName}, 대사: {dialogue}");
        dialoguePanel.SetActive(true);
        npcNameText.text = npcName;
        dialogueText.text = dialogue;
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public void ShowMiniGameInfo(string gameName, Sprite gameImage, int bestScore)
    {
        miniGamePanel.SetActive(true);
        miniGameNameText.text = gameName;
        miniGameImage.sprite = gameImage;
        bestScoreText.text = $"최고 기록: {bestScore}";
    }

    public void HideMiniGameInfo()
    {
        miniGamePanel.SetActive(false);
    }


    public void ContinueDialogue()
    {
        // 현재 진행 중인 대화를 계속 진행할 NPC를 찾음
        NPCController activeNPC = FindObjectOfType<NPCController>();
        if (activeNPC != null)
        {
            activeNPC.ContinueConversation(); // NPC의 대화 진행 함수 호출
        }
    }

    public bool IsDialogueActive()
    {
        return dialoguePanel.activeSelf;
    }

    public bool IsMiniGameInfoActive()
    {
        return miniGamePanel.activeSelf; // 미니게임 정보창이 활성화되어 있는지 확인
    }
}
