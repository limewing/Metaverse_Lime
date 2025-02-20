using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : BaseController
{
    [SerializeField] private DialogueData dialogueData;

    private bool isPlayerNear = false;
    private bool isTalking = false;
    private int dialogueIndex = 0;
    private Transform player;

    protected override void Start()
    {
        base.Start();
        lookDirection = Vector2.down; // �⺻������ �Ʒ����� �ٶ�
    }

    private void Update()
    {
        if (isTalking) return; // ��ȭ ���� �� ��ȣ�ۿ� �Ұ�

        if (isPlayerNear && Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Z Ű �Է� ������! ��ȭ ����");
            StartConversation();
        }
    }

    private void StartConversation()
    {
        isTalking = true;
        dialogueIndex = 0;

        // �÷��̾� ������ �ٶ󺸰� ����
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        Rotate(directionToPlayer);

        MainUIManager.Instance.ShowDialogue(dialogueData.npcName, dialogueData.dialogues[dialogueIndex]); // ��ȭ ����

        if (player.TryGetComponent(out PlayerController playerController))
            playerController.SetTalkingState(true);
    }

    public void ContinueConversation()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogueData.dialogues.Length)
        {
            Debug.Log($"ContinueDialogue() ����� - ���� ���: {dialogueData.dialogues[dialogueIndex]}");
            MainUIManager.Instance.ShowDialogue(dialogueData.npcName, dialogueData.dialogues[dialogueIndex]);
        }
        else
        {
            Debug.Log("��ȭ�� �������Ƿ� �̴ϰ��� ������ ǥ���ؾ� �մϴ�!");
            ShowMiniGameInfo();
        }
    }

    private void ShowMiniGameInfo()     // �̴ϰ��� ������ ���ۿ��� ����� UI�� ��� ��������?
    {
        if (dialogueData.miniGameName == "" || dialogueData.miniGameImage == null)
        {
            Debug.LogError("MiniGame �����Ͱ� ��� ǥ���� �� �����ϴ�!");
            EndConversation();
            return;
        }

        MainUIManager.Instance.ShowMiniGameInfo(dialogueData.miniGameName, dialogueData.miniGameImage, dialogueData.bestScore);
    }

    private void EndConversation()
    {
        isTalking = false;
        lookDirection = Vector2.down; // �ٽ� �Ʒ��� �ٶ󺸵��� ����
        Rotate(lookDirection);
        MainUIManager.Instance.HideDialogue();
        if (player.TryGetComponent(out PlayerController playerController))
            playerController.SetTalkingState(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("NPC ��ó�� �÷��̾� ������");
            isPlayerNear = true;
            player = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("NPC ��ó���� �÷��̾� ���");
            isPlayerNear = false;
        }
    }
}
