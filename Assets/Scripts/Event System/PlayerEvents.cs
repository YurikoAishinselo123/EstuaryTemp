using System;

public static class PlayerEvents
{
    public static event Action<MovementMode> OnChangeMovementMode;
    public static event Action OnStopMovement;
    public static event Action OnResumeMovement;
    public static event Action OnJumpRequested;
    public static event Action<bool> OnSprintToggled;

    public static void RaiseChangeMovementMode(MovementMode mode)
    {
        OnChangeMovementMode?.Invoke(mode);
    }

    public static void RaiseStopMovement()
    {
        OnStopMovement?.Invoke();
    }

    public static void RaiseResumeMovement()
    {
        OnResumeMovement?.Invoke();
    }

    public static void RaiseJumpRequested()
    {
        OnJumpRequested?.Invoke();
    }

    public static void RaiseSprintToggled(bool isSprinting)
    {
        OnSprintToggled?.Invoke(isSprinting);
    }
}
