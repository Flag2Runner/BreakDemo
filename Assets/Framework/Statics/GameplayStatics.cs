using System;
using UnityEngine;

public static class GameplayStatics
{
    internal static void SetGamePaused(bool bIsPaused)
    {
        Time.timeScale = bIsPaused ? 0 : 1 ;
    }
}
