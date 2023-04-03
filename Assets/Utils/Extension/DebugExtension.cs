using UnityEngine;

public static class Debug
{
    public static void Log(object message)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        UnityEngine.Debug.Log(message);
#endif
    }

    public static void Log(object message, Object context)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        UnityEngine.Debug.Log(message, context);
#endif
    }

    public static void LogWarning(object message)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        UnityEngine.Debug.LogWarning(message);
#endif
    }

    public static void LogWarning(object message, Object context)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        UnityEngine.Debug.LogWarning(message, context);
#endif
    }

    public static void LogError(object message)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        UnityEngine.Debug.LogError(message);
#endif
    }

    public static void LogError(object message, Object context)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        UnityEngine.Debug.LogError(message, context);
#endif
    }

    public static void LogException(System.Exception exception)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        UnityEngine.Debug.LogException(exception);
#endif
    }

    public static void LogException(System.Exception exception, Object context)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        UnityEngine.Debug.LogException(exception, context);
#endif
    }
}