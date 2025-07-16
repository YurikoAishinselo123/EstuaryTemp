using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private void Update()
    {
        if (InputManager.Instance.Interact)
        {
            InteractionEvents.RaiseInteract();
        }

        if (InputManager.Instance.Action)
        {
            InteractionEvents.RaiseUseTool();
        }

        // for (int i = 0; i < 9; i++)
        // {
        //     if (InputManager.Instance.IsInventorySlotPressed(i))
        //     {
        //         InteractionEvents.RaiseInventorySlotSelected(i);
        //     }
        // }
    }
}
