using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public abstract class StackBaseUI : MonoBehaviour
{
    protected StackUIManager stackuiManager;

    public virtual void Init(StackUIManager stackuiManager)
    {
        this.stackuiManager = stackuiManager;
    }

    protected abstract UIState GetUIState();

    public void SetActive(UIState state)
    {
        gameObject.SetActive(GetUIState() == state);
    }

}
