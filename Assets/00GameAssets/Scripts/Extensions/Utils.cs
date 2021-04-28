using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KoxUtils {
    public static void ClearConsole()
    {
        var logEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
    
        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
    
        clearMethod.Invoke(null, null);
    }
}