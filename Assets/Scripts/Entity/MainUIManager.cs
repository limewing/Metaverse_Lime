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
            Debug.LogError("MainUIManager�� DialoguePanel�� �������� �ʾҽ��ϴ�");
        }

    }

    public void ShowDialogue(string npcName, string dialogue)
    {
        Debug.Log($"ShowDialogue() ����� - NPC: {npcName}, ���: {dialogue}");
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
        bestScoreText.text = $"�ְ� ���: {bestScore}";
    }

    public void HideMiniGameInfo()
    {
        miniGamePanel.SetActive(false);
    }


    public void ContinueDialogue()
    {
        // ���� ���� ���� ��ȭ�� ��� ������ NPC�� ã��
        NPCController activeNPC = FindObjectOfType<NPCController>();
        if (activeNPC != null)
        {
            activeNPC.ContinueConversation(); // NPC�� ��ȭ ���� �Լ� ȣ��
        }
    }

    public bool IsDialogueActive()
    {
        return dialoguePanel.activeSelf;
    }

    public bool IsMiniGameInfoActive()
    {
        return miniGamePanel.activeSelf; // �̴ϰ��� ����â�� Ȱ��ȭ�Ǿ� �ִ��� Ȯ��
    }
}
