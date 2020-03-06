using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows to display console content in a enhanced way.
/// </summary>
public static class EnhancedConsole
{
    private const string MESSAGE_FORMAT = "[<color=#{0:X2}{1:X2}{2:X2}>{3}</color>] {4}";
    private const float MIN_HUE = 0.0f;
    private const float MAX_HUE = 1.0f;
    private const float MIN_SATURATION = 0.4f;
    private const float MAX_SATURATION = 1.0f;
    private const float MIN_VALUE = 0.4f;
    private const float MAX_VALUE = 1.0f;

    private delegate void LogMethod(string message, Object context);

    private static Dictionary<string, Color> s_TagDictionary = new Dictionary<string, Color>();

    /// <summary>
    /// Logs an info into the Unity Console with a unique tag color.
    /// </summary>
    public static void LogInfo(in string tag, in string info, in Object context = null)
    {
        Log(Debug.Log, in tag, in info, in context);
    }

    /// <summary>
    /// Logs a warning message into the Unity Console with a unique tag color.
    /// </summary>
    public static void LogWarning(in string tag, in string warning, in Object context = null)
    {
        Log(Debug.LogWarning, in tag, in warning, in context);
    }

    /// <summary>
    /// Logs an error message into the Unity Console with a unique tag color.
    /// </summary>
    public static void LogError(in string tag, in string error, in Object context = null)
    {
        Log(Debug.LogError, in tag, in error, in context);
    }

    /// <summary>
    /// Logs an error message into the Unity Console.
    /// </summary>
    public static void LogException(in System.Exception exception, in Object context = null)
    {
        Debug.LogException(exception, context);
    }

    private static Color GetTagColor(in string tag)
    {
        if ( !s_TagDictionary.ContainsKey(tag) )
        {
            Color generatedColor = Random.ColorHSV(MIN_HUE, MAX_HUE, MIN_SATURATION, MAX_SATURATION, MIN_VALUE, MAX_VALUE);
            s_TagDictionary.Add(tag, generatedColor);
        }
        return s_TagDictionary[tag];
    }

    private static void Log(in LogMethod logMethod, in string tag, in string message, in Object context)
    {
        Color tagColor = GetTagColor(in tag);
        byte red = (byte)(tagColor.r * 255f);
        byte green = (byte)(tagColor.g * 255f);
        byte blue = (byte)(tagColor.b * 255f);

        logMethod(string.Format(MESSAGE_FORMAT, red, green, blue, tag, message), context);
    }
}
