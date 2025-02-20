using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum UIState
{ 
    Home,
    Game,
    Score,
}
public class StackUIManager : MonoBehaviour
{
    static StackUIManager instance;

    public static StackUIManager Instance
    {
        get { return instance; }
    }

    UIState currentState = UIState.Home;
    StackHomeUI stackHomeUI = null;
    StackGameUI stactGameUI = null;
    StackScoreUI stackScoreUI = null;

    TheStack theStack = null;

    private void Awake()
    {
        instance = this;

        theStack = FindObjectOfType<TheStack>();

        stackHomeUI = GetComponentInChildren<StackHomeUI>(true);      // true일 경우 꺼져있는 오브젝트도 찾음
        stackHomeUI?.Init(this);

        stactGameUI = GetComponentInChildren<StackGameUI>(true);
        stactGameUI?.Init(this);

        stackScoreUI = GetComponentInChildren<StackScoreUI>(true);
        stackScoreUI?.Init(this);

        ChangeState(UIState.Home);
    }

    public void ChangeState(UIState state)
    {
        currentState = state;
        stackHomeUI?.SetActive(currentState);
        stactGameUI?.SetActive(currentState);
        stackScoreUI?.SetActive(currentState);
    }

    public void OnClickStart()
    {
        theStack.Restart();
        ChangeState(UIState.Game);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void UpdateScore()
    {
        stactGameUI.SetUI(theStack.Score, theStack.Combo, theStack.MaxCombo);
    }

    public void SetScoreUI()
    {
        stackScoreUI.SetUI(theStack.Score, theStack.MaxCombo, theStack.BestScore, theStack.BestCombo);
        ChangeState(UIState.Score);
    }
}
