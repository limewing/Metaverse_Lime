using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public string npcName; // NPC 이름
    public string[] dialogues; // 대화 리스트
    public string miniGameName; // 미니게임 이름
    public Sprite miniGameImage; // 미니게임 이미지
    public Sprite miniGameBackImage;    //  미니게임 설명UI 배경화면
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
