using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup GameplayControl;
    [SerializeField] CanvasGroup GameplayMenu;
    
    public void SetGameplayControlEnable(bool enable)
    {
        SetCanvasGroupEnable(GameplayControl, enable);
    }

    public void SetGameplayMenuEnable(bool enable)
    {
        SetCanvasGroupEnable(GameplayMenu, enable);
    }

    private void SetCanvasGroupEnable(CanvasGroup grp, bool enable)
    {
        grp.interactable = enable;
        grp.blocksRaycasts = enable;
    }
}
