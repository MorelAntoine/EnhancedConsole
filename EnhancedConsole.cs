using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows to log messages into the Unity Console with distinct colors.
/// </summary>
public static class EnhancedConsole
{
    private const string MESSAGE_FORMAT = "[<color=#{0}>{1}</color>] <color=#{2}>{3}</color>";
    private const string INFO_COLOR_CODE = "fcfcfc";
    private const string WARNING_COLOR_CODE = "ffe44c";
    private const string ERROR_COLOR_CODE = "b10c0c";
    private const float MIN_HUE = 0.0f;
    private const float MAX_HUE = 0.89f;
    private const float MIN_SATURATION = 0.66f;
    private const float MAX_SATURATION = 0.99f;
    private const float MIN_VALUE = 0.42f;
    private const float MAX_VALUE = 0.98f;

    private delegate void LogMethod(string message, Object context);

    private static Dictionary<string, Color> s_TagDictionary = new Dictionary<string, Color>();

    /// <summary>
    /// Logs an info into the Unity Console with distinct colors.
    /// </summary>
    public static void LogInfo(in string tag, in string info, in Object context = null)
    {
        Log(Debug.Log, in tag, in info, INFO_COLOR_CODE, in context);
    }

    /// <summary>
    /// Logs a warning into the Unity Console with distinct colors.
    /// </summary>
    public static void LogWarning(in string tag, in string warning, in Object context = null)
    {
        Log(Debug.LogWarning, in tag, in warning, WARNING_COLOR_CODE, in context);
    }

    /// <summary>
    /// Logs an error into the Unity Console with distinct colors.
    /// </summary>
    public static void LogError(in string tag, in string error, in Object context = null)
    {
        Log(Debug.LogError, in tag, in error, ERROR_COLOR_CODE, in context);
    }

    /// <summary>
    /// Logs an exception into the Unity Console.
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

    private static void Log(in LogMethod logMethod, in string tag, in string message, in string messageColorCode, in Object context)
    {
        Color tagColor = GetTagColor(in tag);
        string tagColorCode = ColorUtility.ToHtmlStringRGB(tagColor);

        logMethod(string.Format(MESSAGE_FORMAT, tagColorCode, tag, messageColorCode, message), context);
    }
}
