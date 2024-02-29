using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Developer
{
    [MenuItem("Developer/Clear Saves")]
    public static void ClearSaves()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All saves have been cleared");
    }

    [MenuItem("Developer/Save Prefs")]
    public static void SavePrefs()
    {
        PlayerPrefs.Save();
        Debug.Log("All saves have been cleared");
    }
}
