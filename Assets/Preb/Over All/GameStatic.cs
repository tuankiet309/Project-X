using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStatic 
{
    public static void SetGamePause(bool pause)
    {
        Time.timeScale = pause? 0 :1;
    }
}
