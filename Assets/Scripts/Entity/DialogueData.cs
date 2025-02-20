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
    public Sprite miniGameBackImage;    //  �̴ϰ��� ����UI ���ȭ��
    public int bestScore
    {
        get { return PlayerPrefs.GetInt("BestScore", 0); }
        set
        {
            PlayerPrefs.SetInt("BestScore", value);
            PlayerPrefs.Save();
        }
    }
}
