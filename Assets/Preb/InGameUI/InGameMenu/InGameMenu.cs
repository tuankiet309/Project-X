using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] Button resumeBTN;
    [SerializeField] Button restartBTN;
    [SerializeField] Button menuBTN;
    [SerializeField] UIManager manager;
    [SerializeField] LevelManager levelManager;
    private void Start()
    {
        resumeBTN.onClick.AddListener(ResumeLevel);
        restartBTN.onClick.AddListener(RestartLevel);
        menuBTN.onClick.AddListener(GoToMenu);
    }

    private void GoToMenu()
    {
        levelManager.GoToMainMenu();
    }

    private void RestartLevel()
    {
        levelManager.RestartCurrentLevel();
    }

    private void ResumeLevel()
    {
        manager.SwitchToGameplay();
    }
}
