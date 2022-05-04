using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    internal static GameObject playerReference;
    internal static List<Item> inventory = new List<Item>();
    internal static List<string> historyLog = new List<string>();
    internal static bool readyForNextLevel;

    internal static readonly string[] levelName = {"Morgue", "Library"};
    internal static string currentLevelName;

    public static void NewGame()
    {
        //Clear inventory and history log
        inventory = new List<Item>();
        DialogController.main.ClearLog();
    }
}
