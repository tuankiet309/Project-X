using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup GameplayControl;
    [SerializeField] CanvasGroup GameplayMenu;
    [SerializeField] CanvasGroup Shop;

    CanvasGroup currentActiveGroup;

    List<CanvasGroup> AllChildren = new List<CanvasGroup>();

    private void Start()
    {
        List<CanvasGroup> children = new List<CanvasGroup>();
        GetComponentsInChildren(true, children);
        foreach (CanvasGroup child in children)
        {
            if(child.transform.parent == transform)
            {
                AllChildren.Add(child);
                SetGroupActive(child, false, false);
            }
        }
        if(AllChildren.Count > 0)
        {
            SetCurrentActiveGroup(AllChildren[0]);
            currentActiveGroup = GameplayControl;
        }
    }

    private void SetCurrentActiveGroup(CanvasGroup canvasGroup)
    {
        if (currentActiveGroup != null)
        {
            SetGroupActive(currentActiveGroup,false,false);

        }
        SetGroupActive(canvasGroup,true,true);
        currentActiveGroup = canvasGroup;
    }

    private void SetGroupActive(CanvasGroup child, bool interactable, bool visible)
    {
        child.interactable = interactable;
        child.blocksRaycasts = interactable;
        child.alpha = visible ? 1 : 0;
    }

    internal void SwitchToShop()
    {
        SetCurrentActiveGroup(Shop);
    }

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

    internal void SwitchToGameplay()
    {
        SetCurrentActiveGroup(GameplayControl);
    }
}
