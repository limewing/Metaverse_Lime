using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private bool isTalking = false;

    
    protected override void HandleAction()
    {
        if (isTalking) return;

        // �Է��� ������ �÷��̾� �̵�
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;

        // �̵��� ��쿡�� lookDirection ����
        if (movementDirection != Vector2.zero)
        {
            lookDirection = movementDirection;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (MainUIManager.Instance.IsDialogueActive())
            {
                Debug.Log("Z Ű �Է� ������ - ��ȭ ����");
                NPCController activeNPC = FindObjectOfType<NPCController>(); // ���� ��ȭ ���� NPC ã��
                if (activeNPC != null)
                {
                    activeNPC.ContinueConversation();
                }
            }
            else if (MainUIManager.Instance.IsMiniGameInfoActive())
            {
                Debug.Log("Z Ű �Է� ������ - �̴ϰ��� ���� �ݱ�");
                MainUIManager.Instance.HideMiniGameInfo();
                NPCController activeNPC = FindObjectOfType<NPCController>(); // ���� ��ȭ ���� NPC ã��
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
