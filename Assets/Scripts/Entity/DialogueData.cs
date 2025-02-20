using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public string npcName; // NPC �̸�
    public string[] dialogues; // ��ȭ ����Ʈ
    public string miniGameName; // �̴ϰ��� �̸�
    public Sprite miniGameImage; // �̴ϰ��� �̹���
    public int bestScore; // �ְ� ���
}
