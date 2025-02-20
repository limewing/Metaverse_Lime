using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private bool isTalking = false;

    
    protected override void HandleAction()
    {
        if (isTalking) return;

        // 입력을 받으면 플레이어 이동
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;

        // 이동한 경우에만 lookDirection 갱신
        if (movementDirection != Vector2.zero)
        {
            lookDirection = movementDirection;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (MainUIManager.Instance.IsDialogueActive())
            {
                Debug.Log("Z 키 입력 감지됨 - 대화 진행");
                NPCController activeNPC = FindObjectOfType<NPCController>(); // 현재 대화 중인 NPC 찾기
                if (activeNPC != null)
                {
                    activeNPC.ContinueConversation();
                }
            }
            else if (MainUIManager.Instance.IsMiniGameInfoActive())
            {
                Debug.Log("Z 키 입력 감지됨 - 미니게임 정보 닫기");
                MainUIManager.Instance.HideMiniGameInfo();
                NPCController activeNPC = FindObjectOfType<NPCController>(); // 현재 대화 중인 NPC 찾기
                if (activeNPC != null)
                {
                    activeNPC.EndConversation();
                }
            }
        }
    }

    public void SetTalkingState(bool state)
    {
        isTalking = state;
    }
}
