using System;

public static class InteractionEvents
{
    public static event Action OnInteract;
    public static event Action OnUseTool;
    public static event Action<int> OnInventorySlotSelected;

    public static void RaiseInteract()
    {
        OnInteract?.Invoke();
    }

    public static void RaiseUseTool()
    {
        OnUseTool?.Invoke();
    }

    public static void RaiseInventorySlotSelected(int index)
    {
        OnInventorySlotSelected?.Invoke(index);
    }
}
