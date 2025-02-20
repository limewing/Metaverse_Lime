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
        lookDirection = Vector2.down; // 기본적으로 아래쪽을 바라봄
    }

    private void Update()
    {
        if (isPlayerNear)
        {
            if (!isTalking && Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("Z 키 입력 감지됨! 대화 시작");
                StartConversation();
            }
            else if (isTalking && Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("Z 키 입력 감지됨! 대화 계속");
                ContinueConversation();
            }
            else if (isTalking && Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("X 키 입력 감지됨! 대화 종료");
                EndConversation();
            }
        }
    }

    private void StartConversation()
    {
        isTalking = true;
        dialogueIndex = 0;

        // 플레이어 방향을 바라보게 설정
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        Rotate(directionToPlayer);

        MainUIManager.Instance.ShowDialogue(dialogueData.npcName, dialogueData.dialogues[dialogueIndex]); // 대화 시작

        if (player.TryGetComponent(out PlayerController playerController))
            playerController.SetTalkingState(true);
    }

    public void ContinueConversation()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogueData.dialogues.Length)
        {
            Debug.Log($"ContinueDialogue() 실행됨 - 현재 대사: {dialogueData.dialogues[dialogueIndex]}");
            MainUIManager.Instance.ShowDialogue(dialogueData.npcName, dialogueData.dialogues[dialogueIndex]);
        }
        else
        {
            Debug.Log("대화가 끝났으므로 미니게임 정보를 표시해야 합니다!");
            ShowMiniGameInfo();
        }
    }

    private void ShowMiniGameInfo()     // 미니게임 정보와 시작여부 물어보는 UI는 어디에 만들어두지?
    {
        if (dialogueData.miniGameName == "" || dialogueData.miniGameImage == null)
        {
            Debug.LogError("MiniGame 데이터가 없어서 표시할 수 없습니다!");
            EndConversation();
            return;
        }
        Debug.Log($"미니게임 정보 표시: {dialogueData.miniGameName} / 최고 점수: {dialogueData.bestScore}");
        MainUIManager.Instance.ShowMiniGameInfo(dialogueData.miniGameBackImage ,dialogueData.miniGameName, dialogueData.miniGameImage);
    }

    public void EndConversation()
    {
        isTalking = false;
        MainUIManager.Instance.HideMiniGameInfo();
        lookDirection = Vector2.down; // 다시 아래를 바라보도록 설정
        Rotate(lookDirection);
        MainUIManager.Instance.HideDialogue();
        if (player.TryGetComponent(out PlayerController playerController))
            playerController.SetTalkingState(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("NPC 근처에 플레이어 감지됨");
            isPlayerNear = true;
            player = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("NPC 근처에서 플레이어 벗어남");
            isPlayerNear = false;
        }
    }
}
