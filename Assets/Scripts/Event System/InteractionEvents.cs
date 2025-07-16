using System;

public static class InteractionEvents
{
    public static event Action OnInteract;

    public static void RaiseInteract()
    {
        OnInteract?.Invoke();
    }
}
