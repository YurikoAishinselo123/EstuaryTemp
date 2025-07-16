using System;

public static class DetectionEvents
{
    public static event Action<string> OnDetect;
    public static event Action OnDetectCleared;

    public static event Action<string> OnFactDetected;
    public static event Action OnFactCleared;

    public static void RaiseDetect(string displayName)
    {
        OnDetect?.Invoke(displayName);
    }

    public static void RaiseDetectCleared()
    {
        OnDetectCleared?.Invoke();
    }

    public static void RaiseFactDetected(string fact)
    {
        OnFactDetected?.Invoke(fact);
    }

    public static void RaiseFactCleared()
    {
        OnFactCleared?.Invoke();
    }
}
